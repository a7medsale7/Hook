using Hangfire;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Booking;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class BookingService(
    IBookingRepository bookingRepository,
    ITripDateRepository tripDateRepository,
    ITripRepository tripRepository,
    IBoatOwnerRepository boatOwnerRepository,
    IEmailSender emailSender,
    IBackgroundJobClient backgroundJobClient,
    IUnitOfWork unitOfWork) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly ITripDateRepository _tripDateRepository = tripDateRepository;
    private readonly ITripRepository _tripRepository = tripRepository;
    private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BookingResponse>> CreateBookingAsync(string userId, CreateBookingRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Get Trip Date with Trip details
        var tripDate = await _tripDateRepository.GetByIdAsync(request.TripDateId);
        if (tripDate is null)
            return Result.Failure<BookingResponse>(TripErrors.DateNotFound);

        // --- CRITICAL VALIDATIONS (TRIPLE CHECK) ---
        
        // Check 1: Inactive or Past Date
        if (!tripDate.IsActive)
            return Result.Failure<BookingResponse>(BookingErrors.TripDateInactive);

        if (tripDate.StartDate < DateTime.UtcNow)
            return Result.Failure<BookingResponse>(BookingErrors.TripDatePassed);

        // Check 2: Seat Availability
        if (tripDate.AvailableSeats < request.NumberOfParticipants)
            return Result.Failure<BookingResponse>(BookingErrors.InsufficientSeats);

        // Check 3: Duplicate Booking Prevention
        if (await _bookingRepository.ExistsForUserAndDateAsync(userId, request.TripDateId))
            return Result.Failure<BookingResponse>(BookingErrors.AlreadyBooked);

        // 2. Begin Atomic Process
        var trip = tripDate.Trip;
        var totalPrice = trip.PricePerPerson * request.NumberOfParticipants;

        var booking = new Booking
        {
            UserId = userId,
            TripDateId = request.TripDateId,
            NumberOfParticipants = request.NumberOfParticipants,
            TotalPrice = totalPrice,
            Status = BookingStatus.Pending,
            SpecialRequests = request.SpecialRequests
        };

        var payment = new Payment
        {
            BookingId = booking.Id,
            Amount = totalPrice,
            Status = PaymentStatus.Pending,
            PaymentMethod = request.PaymentMethod
        };
        booking.Payment = payment;

        // Atomic Update: Decrease Available Seats
        tripDate.AvailableSeats -= request.NumberOfParticipants;

        await _bookingRepository.AddAsync(booking);
        _tripDateRepository.Update(tripDate);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);



        // 4. Return result
        var detailedBooking = await _bookingRepository.GetByIdWithDetailsAsync(booking.Id);
        return Result.Success(ToResponse(detailedBooking!));
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetMyBookingsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return Result.Success(bookings.Select(ToResponse));
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetTripBookingsAsync(Guid tripDateId, string userId, CancellationToken cancellationToken = default)
    {
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<IEnumerable<BookingResponse>>(TripErrors.NoBoatAvailable);

        var tripDate = await _tripDateRepository.GetByIdAsync(tripDateId);
        if (tripDate is null)
            return Result.Failure<IEnumerable<BookingResponse>>(TripErrors.DateNotFound);

        // Check ownership
        if (tripDate.Trip.TripManagerId != ownerProfile.Id)
            return Result.Failure<IEnumerable<BookingResponse>>(TripErrors.Unauthorized);

        var bookings = await _bookingRepository.GetByTripDateIdAsync(tripDateId);
        return Result.Success(bookings.Select(ToResponse));
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetAllBookingsAsync(CancellationToken cancellationToken = default)
    {
        var bookings = await _bookingRepository.GetAllWithDetailsAsync();
        return Result.Success(bookings.Select(ToResponse));
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetFilteredBookingsAsync(BookingFilterRequest filter, string? userId = null, Guid? ownerId = null, string? ownerUserId = null, CancellationToken cancellationToken = default)
    {
        if (ownerUserId != null && ownerId == null)
        {
            var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(ownerUserId);
            if (ownerProfile != null)
                ownerId = ownerProfile.Id;
        }

        var bookings = await _bookingRepository.GetAllFilteredAsync(
            filter.Status, 
            filter.Location, 
            filter.Date, 
            userId, 
            ownerId);
            
        return Result.Success(bookings.Select(ToResponse));
    }

    public async Task<Result<BookingStatsResponse>> GetBookingStatsAsync(string userId, bool isAdmin, CancellationToken cancellationToken = default)
    {
        Guid? ownerId = null;
        if (!isAdmin)
        {
            var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
            if (ownerProfile is null)
                return Result.Failure<BookingStatsResponse>(TripErrors.NoBoatAvailable);
            ownerId = ownerProfile.Id;
        }

        // Logic: 
        // - Admin: userId null, ownerId null (See everything)
        // - Owner: userId null, ownerId set (See everything on their boats)
        // - User:  userId set, ownerId null (See only their bookings)
        string? filterUserId = isAdmin ? null : (ownerId == null ? userId : null);

        var bookings = await _bookingRepository.GetAllFilteredAsync(userId: filterUserId, ownerId: ownerId);
        
        var stats = new BookingStatsResponse(
            TotalBookings: bookings.Count(),
            PendingBookings: bookings.Count(b => b.Status == BookingStatus.Pending),
            ApprovedBookings: bookings.Count(b => b.Status == BookingStatus.Confirmed),
            RejectedBookings: bookings.Count(b => b.Status == BookingStatus.Rejected),
            CompletedBookings: bookings.Count(b => b.Status == BookingStatus.Completed),
            CancelledBookings: bookings.Count(b => b.Status == BookingStatus.Cancelled),
            TotalRevenue: bookings.Where(b => b.Status == BookingStatus.Completed || b.Status == BookingStatus.Confirmed)
                                  .Sum(b => b.TotalPrice)
        );

        return Result.Success(stats);
    }

    public async Task<Result<BookingResponse>> UpdateBookingStatusAsync(Guid id, string userId, UpdateBookingStatusRequest request, bool isAdmin = false, CancellationToken cancellationToken = default)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);
        if (booking is null)
            return Result.Failure<BookingResponse>(BookingErrors.NotFound);

        // Authorization: Admin or the Boat Owner who manages this trip
        if (!isAdmin)
        {
            var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
            if (ownerProfile is null || booking.TripDate.Trip.TripManagerId != ownerProfile.Id)
                return Result.Failure<BookingResponse>(BookingErrors.Unauthorized);

            // Restriction 1: Owners cannot set status to Cancelled (reserved for Users/Admin)
            if (request.Status == BookingStatus.Cancelled)
                return Result.Failure<BookingResponse>(new Error("Booking.OwnerCannotCancel", "Owners cannot cancel bookings. They can only reject pending ones."));

            // Restriction 2: Owners cannot change a booking that is already Confirmed or Completed
            if (booking.Status == BookingStatus.Confirmed || booking.Status == BookingStatus.Completed)
                return Result.Failure<BookingResponse>(new Error("Booking.StatusLocked", "Confirmed or Completed bookings cannot be modified by the owner."));
        }

        // Logic check: If rejected, restore seats
        if (request.Status == BookingStatus.Rejected && booking.Status != BookingStatus.Rejected)
        {
            var tripDate = await _tripDateRepository.GetByIdAsync(booking.TripDateId);
            if (tripDate != null)
            {
                tripDate.AvailableSeats += booking.NumberOfParticipants;
                _tripDateRepository.Update(tripDate);
            }
        }
        
        booking.Status = request.Status;
        _bookingRepository.Update(booking);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Notify User
        try 
        {
            if (booking.User?.Email != null && booking.TripDate?.Trip != null)
            {
                string userName = $"{booking.User.FirstName} {booking.User.LastName}";
                if (request.Status == BookingStatus.Confirmed)
                {
                    string html = Hook.Domain.Helpers.EmailTemplates.GetBookingConfirmedTemplate(
                        userName, booking.TripDate.Trip.Title, booking.TripDate.StartDate, booking.TotalPrice);
                    _backgroundJobClient.Enqueue<IEmailSender>(sender => 
                        sender.SendEmailAsync(booking.User.Email, "✅ Booking Confirmed!", html));
                }
                else if (request.Status == BookingStatus.Rejected)
                {
                    string html = Hook.Domain.Helpers.EmailTemplates.GetBookingRejectedTemplate(
                        userName, booking.TripDate.Trip.Title, "Your booking request was rejected by the owner.");
                    _backgroundJobClient.Enqueue<IEmailSender>(sender => 
                        sender.SendEmailAsync(booking.User.Email, "⚠️ Booking Rejected", html));
                }
            }
        }
        catch { /* Log failure but don't fail */ }

        return Result.Success(ToResponse(booking));
    }

    public async Task<Result> CancelBookingAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);
        if (booking is null)
            return Result.Failure(BookingErrors.NotFound);

        if (booking.UserId != userId)
            return Result.Failure(BookingErrors.Unauthorized);

        if (booking.Status == BookingStatus.Cancelled || booking.Status == BookingStatus.CancellationRequested)
            return Result.Failure(BookingErrors.AlreadyCancelled);

        // --- المنطق الجديد ---
        
        // 1. لو الحجز لسه Pending (مستني موافقة أو لسه مدفعش)
        if (booking.Status == BookingStatus.Pending)
        {
            // نرجّع الكراسي فوراً
            var tripDate = await _tripDateRepository.GetByIdAsync(booking.TripDateId);
            if (tripDate != null)
            {
                tripDate.AvailableSeats += booking.NumberOfParticipants;
                _tripDateRepository.Update(tripDate);
            }

            booking.Status = BookingStatus.Cancelled;
            if (booking.Payment != null)
            {
                booking.Payment.Status = PaymentStatus.Rejected; // نعتبرها فشلت أو اترفضت
                booking.Payment.AdminNotes = "Cancelled by user before confirmation";
            }
        }
        // 2. لو الحجز مؤكد (يعني دفع والفلوس اتقبلت)
        else if (booking.Status == BookingStatus.Confirmed || booking.Status == BookingStatus.Completed)
        {
            // نغير الحالة لـ "طلب إلغاء" ونستنى صاحب المركب
            booking.Status = BookingStatus.CancellationRequested;
        }
        else 
        {
            return Result.Failure(new Error("Booking.CannotCancel", "This booking cannot be cancelled in its current status."));
        }

        _bookingRepository.Update(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetMyCancelledBookingsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        var cancelledOnes = bookings.Where(b => b.Status == BookingStatus.Cancelled || b.Status == BookingStatus.CancellationRequested);
        return Result.Success(cancelledOnes.Select(ToResponse));
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetCancellationRequestsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<IEnumerable<BookingResponse>>(TripErrors.NoBoatAvailable);

        var bookings = await _bookingRepository.GetAllFilteredAsync(status: BookingStatus.CancellationRequested, ownerId: ownerProfile.Id);
        return Result.Success(bookings.Select(ToResponse));
    }

    public async Task<Result> HardDeleteBookingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);
        if (booking is null)
            return Result.Failure(BookingErrors.NotFound);

        // If not already cancelled or rejected, revert seats
        if (booking.Status != BookingStatus.Cancelled && booking.Status != BookingStatus.Rejected)
        {
            var tripDate = await _tripDateRepository.GetByIdAsync(booking.TripDateId);
            if (tripDate != null)
            {
                tripDate.AvailableSeats += booking.NumberOfParticipants;
                _tripDateRepository.Update(tripDate);
            }
        }

        _bookingRepository.HardDelete(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private BookingResponse ToResponse(Booking booking) => new BookingResponse(
        booking.Id,
        booking.TripDate.Trip.Title,
        booking.TripDate.Trip.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? booking.TripDate.Trip.Images.FirstOrDefault()?.ImageUrl,
        booking.TripDate.StartDate,
        booking.TripDate.EndDate,
        booking.TripDate.Trip.Boat?.Name ?? "Unknown Boat",
        booking.NumberOfParticipants,
        booking.TotalPrice,
        booking.Status,
        booking.SpecialRequests,
        $"{booking.User?.FirstName} {booking.User?.LastName}",
        booking.User?.PhoneNumber,
        booking.User?.Email,
        booking.Payment == null ? null : new BookingPaymentInfo(
            booking.Payment.Id,
            booking.Payment.Amount,
            booking.Payment.Status,
            booking.Payment.PaymentMethod,
            booking.Payment.TransactionId,
            booking.Payment.ReceiptImageUrl
        )
    );
}
