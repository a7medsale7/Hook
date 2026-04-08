using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Review;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IReviewService
{
    Task<Result<ReviewResponse>> CreateAsync(string userId, CreateReviewRequest request, CancellationToken cancellationToken = default);
    Task<Result<ReviewResponse>> UpdateAsync(Guid id, string userId, UpdateReviewRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReviewResponse>>> GetTripReviewsAsync(Guid tripId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReviewResponse>>> GetMyReviewsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, string userId, CancellationToken cancellationToken = default);
}
