using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.BoatOwner.Dashboard;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class BoatOwnerDashboardService(
        IBoatOwnerRepository boatOwnerRepository,
        IBookingRepository bookingRepository,
        ITripRepository tripRepository,
        IReviewRepository reviewRepository) : IBoatOwnerDashboardService
    {
        private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ITripRepository _tripRepository = tripRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public async Task<Result<BoatOwnerStatisticsResponse>> GetStatisticsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var boatOwner = await _boatOwnerRepository.GetByUserIdAsync(userId);
            if (boatOwner is null || boatOwner.Status != RequestStatus.Approved)
                return Result.Failure<BoatOwnerStatisticsResponse>(BoatOwnerErrors.NotApproved);

            // Upcoming Bookings count
            var bookings = await _bookingRepository.GetByOwnerIdAsync(boatOwner.Id);
            var upcomingBookingsCount = bookings.Count(b => b.Status != BookingStatus.Cancelled && b.TripDate.EndDate >= DateTime.UtcNow);

            // Active Trips count
            var trips = await _tripRepository.GetByOwnerIdAsync(boatOwner.Id);
            var activeTripsCount = trips.Count(t => t.TripDates.Any(d => d.IsActive && d.StartDate >= DateTime.UtcNow));

            // Earnings
            var earnings = bookings.Where(b => b.Status == BookingStatus.Completed || b.Status == BookingStatus.Confirmed).Sum(b => b.TotalPrice);

            // Avg Rating
            var reviews = await _reviewRepository.GetByOwnerIdAsync(boatOwner.Id);
            var avgRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;

            return Result.Success(new BoatOwnerStatisticsResponse(
                upcomingBookingsCount,
                activeTripsCount,
                Math.Round(avgRating, 1),
                earnings
            ));
        }

        public async Task<Result<IEnumerable<UpcomingBookingResponse>>> GetUpcomingBookingsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var boatOwner = await _boatOwnerRepository.GetByUserIdAsync(userId);
            if (boatOwner is null || boatOwner.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<UpcomingBookingResponse>>(BoatOwnerErrors.NotApproved);

            var bookings = await _bookingRepository.GetByOwnerIdAsync(boatOwner.Id);
            var upcomingBookings = bookings
                .Where(b => b.Status != BookingStatus.Cancelled && b.TripDate.EndDate >= DateTime.UtcNow)
                .OrderBy(b => b.TripDate.StartDate)
                .Select(b => new UpcomingBookingResponse(
                    b.Id,
                    b.TripDate.Trip.Title,
                    b.TripDate.StartDate,
                    b.TripDate.EndDate,
                    b.NumberOfParticipants,
                    b.TotalPrice,
                    b.Status.ToString()
                ))
                .ToList();

            return Result.Success((IEnumerable<UpcomingBookingResponse>)upcomingBookings);
        }

        public async Task<Result<IEnumerable<ActiveTripResponse>>> GetActiveTripsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var boatOwner = await _boatOwnerRepository.GetByUserIdAsync(userId);
            if (boatOwner is null || boatOwner.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<ActiveTripResponse>>(BoatOwnerErrors.NotApproved);

            var trips = await _tripRepository.GetByOwnerIdAsync(boatOwner.Id);
            
            var activeTrips = trips
                .Where(t => t.TripDates.Any(d => d.IsActive && d.StartDate >= DateTime.UtcNow))
                .Select(t => new ActiveTripResponse(
                    t.Id,
                    t.Title,
                    t.LocationName,
                    t.PricePerPerson,
                    t.TripDates.Count(d => d.IsActive && d.StartDate >= DateTime.UtcNow)
                ))
                .OrderByDescending(t => t.AvailableDatesCount)
                .ToList();

            return Result.Success((IEnumerable<ActiveTripResponse>)activeTrips);
        }
    }
}
