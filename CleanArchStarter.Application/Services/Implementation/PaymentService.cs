using Hangfire;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Payment;
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

public class PaymentService(
    IPaymentRepository paymentRepository,
    IBookingRepository bookingRepository,
    IBoatOwnerRepository boatOwnerRepository,
    IEmailSender emailSender,
    IFileService fileService,
    IBackgroundJobClient backgroundJobClient,
    IUnitOfWork unitOfWork) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IFileService _fileService = fileService;
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaymentResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        if (payment is null)
            return Result.Failure<PaymentResponse>(PaymentErrors.NotFound);

        return Result.Success(MapToResponse(payment));
    }

    public async Task<Result<IEnumerable<PaymentResponse>>> GetMyPaymentsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.GetAllAsync();
        var userPayments = payments.Where(p => p.Booking?.UserId == userId);
        return Result.Success(userPayments.Select(MapToResponse));
    }

    public async Task<Result<IEnumerable<PaymentResponse>>> GetFilteredPaymentsAsync(
        PaymentFilterRequest filter, 
        string? userId = null, 
        Guid? ownerId = null, 
        CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.GetAllAsync();
        var query = payments.AsQueryable();

        if (!string.IsNullOrEmpty(userId))
            query = query.Where(p => p.Booking != null && p.Booking.UserId == userId);

        if (ownerId.HasValue)
            query = query.Where(p => p.Booking != null && 
                                   p.Booking.TripDate != null && 
                                   p.Booking.TripDate.Trip != null && 
                                   p.Booking.TripDate.Trip.TripManagerId == ownerId.Value);

        if (filter.Status.HasValue)
            query = query.Where(p => p.Status == filter.Status.Value);

        if (filter.Method.HasValue)
            query = query.Where(p => p.PaymentMethod == filter.Method.Value);

        if (filter.Date.HasValue)
            query = query.Where(p => p.CreatedOn.Date == filter.Date.Value.Date);

        return Result.Success(query.Select(MapToResponse));
    }

    public async Task<Result<PaymentResponse>> UploadReceiptAsync(Guid id, string userId, UploadReceiptRequest request, CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        if (payment is null)
            return Result.Failure<PaymentResponse>(PaymentErrors.NotFound);

        if (payment.Booking?.UserId != userId)
            return Result.Failure<PaymentResponse>(PaymentErrors.Unauthorized);

        // Only InstaPay payments need receipt upload
        if (payment.PaymentMethod != PaymentMethod.InstaPay)
            return Result.Failure<PaymentResponse>(PaymentErrors.InvalidStatus);

        if (request.ReceiptImage is null || request.ReceiptImage.Length == 0)
            return Result.Failure<PaymentResponse>(PaymentErrors.ReceiptRequired);

        var imageUrl = await _fileService.SaveFileAsync(request.ReceiptImage, "receipts");

        payment.ReceiptImageUrl = imageUrl;
        payment.Status = PaymentStatus.Pending;

        _paymentRepository.Update(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Notify Owner
        var trip = payment.Booking?.TripDate?.Trip;
        if (trip != null)
        {
            var owner = await _boatOwnerRepository.GetByIdAsync(trip.TripManagerId);
            if (owner?.User?.Email != null)
            {
                string ownerName = $"{owner.User.FirstName} {owner.User.LastName}";
                // NOTE: Change 'https://hook.com' to your actual frontend domain
                string actionUrl = $"https://hook.com/dashboard/payments/{payment.Id}";
                string templateHtml = Hook.Domain.Helpers.EmailTemplates.GetReceiptUploadedTemplate(ownerName, trip.Title, payment.Amount, actionUrl);
                
                try 
                {
                    _backgroundJobClient.Enqueue<IEmailSender>(sender => 
                        sender.SendEmailAsync(owner.User.Email, "💰 New Payment Receipt Uploaded", templateHtml));
                } 
                catch { /* Suppress email tracking errors */ }
            }
        }

        return Result.Success(MapToResponse(payment));
    }

    public async Task<Result<PaymentResponse>> VerifyPaymentAsync(Guid id, string userId, VerifyPaymentRequest request, CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        if (payment is null)
            return Result.Failure<PaymentResponse>(PaymentErrors.NotFound);

        // Prevent re-verification
        if (payment.Status != PaymentStatus.Pending)
            return Result.Failure<PaymentResponse>(PaymentErrors.AlreadyVerified);

        // Authorization: must be the Trip Owner
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        bool isOwnerOfTrip = ownerProfile != null &&
                             payment.Booking?.TripDate?.Trip?.TripManagerId == ownerProfile.Id;

        if (!isOwnerOfTrip)
            return Result.Failure<PaymentResponse>(PaymentErrors.Unauthorized);

        payment.Status = request.IsApproved ? PaymentStatus.Completed : PaymentStatus.Rejected;
        payment.AdminNotes = request.Notes;

        if (request.IsApproved && payment.Booking != null)
        {
            payment.Booking.Status = BookingStatus.Confirmed;
            _bookingRepository.Update(payment.Booking);
        }

        _paymentRepository.Update(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Notify User
        try 
        {
            var booking = payment.Booking;
            if (booking?.User?.Email != null && booking?.TripDate?.Trip != null)
            {
                string userName = $"{booking.User.FirstName} {booking.User.LastName}";
                if (request.IsApproved)
                {
                    string html = Hook.Domain.Helpers.EmailTemplates.GetBookingConfirmedTemplate(userName, booking.TripDate.Trip.Title, booking.TripDate.StartDate, payment.Amount);
                    _backgroundJobClient.Enqueue<IEmailSender>(sender => 
                        sender.SendEmailAsync(booking.User.Email, "✅ Booking & Payment Confirmed!", html));
                }
                else
                {
                    string html = Hook.Domain.Helpers.EmailTemplates.GetBookingRejectedTemplate(userName, booking.TripDate.Trip.Title, request.Notes);
                    _backgroundJobClient.Enqueue<IEmailSender>(sender => 
                        sender.SendEmailAsync(booking.User.Email, "⚠️ Booking Payment Issue", html));
                }
            }
        }
        catch { /* Log failure but don't fail the verification */ }

        return Result.Success(MapToResponse(payment));
    }

    public async Task<Result<PaymentStatsResponse>> GetFinancialStatsAsync(string? userId = null, Guid? ownerId = null, CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.GetAllAsync();
        var query = payments.AsQueryable();

        if (!string.IsNullOrEmpty(userId))
            query = query.Where(p => p.Booking != null && p.Booking.UserId == userId);

        if (ownerId.HasValue)
            query = query.Where(p => p.Booking != null && 
                                   p.Booking.TripDate != null && 
                                   p.Booking.TripDate.Trip != null && 
                                   p.Booking.TripDate.Trip.TripManagerId == ownerId.Value);

        var list = query.ToList();

        return Result.Success(new PaymentStatsResponse(
            TotalRevenue: list.Where(p => p.Status == PaymentStatus.Completed).Sum(p => p.Amount),
            PendingVerification: list.Count(p => p.Status == PaymentStatus.Pending && !string.IsNullOrEmpty(p.ReceiptImageUrl)),
            ApprovedPayments: list.Count(p => p.Status == PaymentStatus.Completed),
            RejectedPayments: list.Count(p => p.Status == PaymentStatus.Rejected)
        ));
    }

    public async Task<Result> MarkAsRefundedAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        if (payment is null)
            return Result.Failure(PaymentErrors.NotFound);

        // Authorization: must be the Trip Owner
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        bool isOwnerOfTrip = ownerProfile != null &&
                             payment.Booking?.TripDate?.Trip?.TripManagerId == ownerProfile.Id;

        if (!isOwnerOfTrip)
            return Result.Failure(PaymentErrors.Unauthorized);

        // Can only refund if it was previously completed or pending
        if (payment.Status == PaymentStatus.Rejected || payment.Status == PaymentStatus.Refunded)
            return Result.Failure(PaymentErrors.InvalidStatus);

        payment.Status = PaymentStatus.Refunded;
        
        if (payment.Booking != null)
        {
            payment.Booking.Status = BookingStatus.Cancelled;
            _bookingRepository.Update(payment.Booking);
        }

        _paymentRepository.Update(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static PaymentResponse MapToResponse(Payment payment) => new(
        payment.Id,
        payment.BookingId ?? Guid.Empty,
        payment.Amount,
        payment.Status,
        payment.PaymentMethod,
        payment.TransactionId,
        payment.ReceiptImageUrl,
        payment.AdminNotes,
        payment.CreatedOn
    );
}
