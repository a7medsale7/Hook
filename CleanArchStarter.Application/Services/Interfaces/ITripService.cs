using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Trip;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface ITripService
{
    Task<Result<TripResponse>> CreateTripAsync(string userId, CreateTripRequest request, CancellationToken cancellationToken = default);
    Task<Result<TripResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TripResponse>>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TripResponse>>> SearchTripsAsync(string? query, string? location, DateTime? date, int? participants, decimal? minPrice, decimal? maxPrice, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TripResponse>>> GetMyTripsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<TripResponse>> UpdateTripAsync(Guid id, string userId, UpdateTripRequest request, bool isAdmin = false, CancellationToken cancellationToken = default);
    Task<Result> SoftDeleteTripAsync(Guid id, string userId, bool isAdmin = false, CancellationToken cancellationToken = default);
    
    // TripDate Management
    Task<Result> AddTripDatesAsync(Guid tripId, string userId, AddTripDatesRequest request, CancellationToken cancellationToken = default);
    Task<Result> ToggleTripDateStatusAsync(Guid dateId, string userId, bool isActive, CancellationToken cancellationToken = default);
}
