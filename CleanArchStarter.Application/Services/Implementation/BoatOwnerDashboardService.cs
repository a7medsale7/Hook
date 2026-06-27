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
        ITripRepository tripRepository) : IBoatOwnerDashboardService
    {
        private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ITripRepository _tripRepository = tripRepository;

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
