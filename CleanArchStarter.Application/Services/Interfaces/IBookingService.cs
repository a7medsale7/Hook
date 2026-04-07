using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Booking;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IBookingService
{
    Task<Result<BookingResponse>> CreateBookingAsync(string userId, CreateBookingRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookingResponse>>> GetMyBookingsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookingResponse>>> GetTripBookingsAsync(Guid tripDateId, string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookingResponse>>> GetAllBookingsAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BookingResponse>>> GetFilteredBookingsAsync(BookingFilterRequest filter, string? userId = null, Guid? ownerId = null, string? ownerUserId = null, CancellationToken cancellationToken = default);
    Task<Result<BookingStatsResponse>> GetBookingStatsAsync(string userId, bool isAdmin, CancellationToken cancellationToken = default);
    Task<Result<BookingResponse>> UpdateBookingStatusAsync(Guid id, string userId, UpdateBookingStatusRequest request, bool isAdmin = false, CancellationToken cancellationToken = default);
    Task<Result> CancelBookingAsync(Guid id, string userId, CancellationToken cancellationToken = default);
}
