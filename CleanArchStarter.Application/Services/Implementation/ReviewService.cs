using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Review;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class ReviewService(
    IReviewRepository reviewRepository,
    IBookingRepository bookingRepository,
    IBoatOwnerRepository boatOwnerRepository,
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork) : IReviewService
{
    public async Task<Result<ReviewResponse>> CreateAsync(string userId, CreateReviewRequest request, CancellationToken cancellationToken = default)
    {
        var booking = await bookingRepository.GetByIdWithDetailsAsync(request.BookingId);
        
        if (booking == null)
            return Result.Failure<ReviewResponse>(BookingErrors.NotFound);

        // Security check: Must be the user who made the booking
        if (booking.UserId != userId)
            return Result.Failure<ReviewResponse>(Error.Forbidden);

        // State check: Booking must be completed to be reviewed
        if (booking.Status != BookingStatus.Completed)
        {
            // Fallback checking if the trip date has already ended (in case completion job hasn't run)
            if (booking.TripDate == null || booking.Status != BookingStatus.Confirmed || booking.TripDate.EndDate >= DateTime.UtcNow)
                return Result.Failure<ReviewResponse>(ReviewErrors.NotEligible);
        }

        // Logic check: Ensure no previous review exists for this booking
        var existingReview = await reviewRepository.GetByBookingIdAsync(request.BookingId);
        if (existingReview != null)
            return Result.Failure<ReviewResponse>(ReviewErrors.AlreadyReviewed);

        var review = new Review
        {
            BookingId = request.BookingId,
            TripId = booking.TripDate!.TripId,
            UserId = userId,
            Rating = request.Rating,
            Comment = request.Comment
        };

        await reviewRepository.AddAsync(review);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Fetch user object to build the response properly
        var user = await userManager.FindByIdAsync(userId);
        review.User = user!;
        review.Trip = booking.TripDate.Trip;

        return Result.Success(MapToResponse(review));
    }

    public async Task<Result<ReviewResponse>> UpdateAsync(Guid id, string userId, UpdateReviewRequest request, CancellationToken cancellationToken = default)
    {
        var review = await reviewRepository.GetByIdAsync(id);
        
        if (review == null)
            return Result.Failure<ReviewResponse>(ReviewErrors.NotFound);

        if (review.UserId != userId)
            return Result.Failure<ReviewResponse>(Error.Forbidden);

        review.Rating = request.Rating;
        review.Comment = request.Comment;

        reviewRepository.Update(review);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(review));
    }

    public async Task<Result<IEnumerable<ReviewResponse>>> GetTripReviewsAsync(Guid tripId, CancellationToken cancellationToken = default)
    {
        var reviews = await reviewRepository.GetByTripIdAsync(tripId);
        return Result.Success(reviews.Select(MapToResponse));
    }

    public async Task<Result<IEnumerable<ReviewResponse>>> GetMyReviewsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var reviews = await reviewRepository.GetByUserIdAsync(userId);
        return Result.Success(reviews.Select(MapToResponse));
    }

    public async Task<Result> DeleteAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        var review = await reviewRepository.GetByIdAsync(id);
        
        if (review == null)
            return Result.Failure(ReviewErrors.NotFound);

        // Security check: Only author, admin, or the trip owner can delete
        var user = await userManager.FindByIdAsync(userId);
        var isAdmin = user != null && await userManager.IsInRoleAsync(user, "Admin");

        var isTripOwner = false;
        if (review.Trip != null)
        {
            var ownerProfile = await boatOwnerRepository.GetByUserIdAsync(userId);
            if (ownerProfile != null && ownerProfile.Id == review.Trip.TripManagerId)
            {
                isTripOwner = true;
            }
        }

        if (review.UserId != userId && !isAdmin && !isTripOwner)
            return Result.Failure(Error.Forbidden);

        reviewRepository.Delete(review);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static ReviewResponse MapToResponse(Review review)
    {
        return new ReviewResponse
        {
            Id = review.Id,
            TripId = review.TripId,
            BookingId = review.BookingId,
            TripName = review.Trip?.Title ?? "Unknown Trip",
            UserName = review.User != null ? $"{review.User.FirstName} {review.User.LastName}".Trim() : "Unknown User",
            UserImage = review.User?.ProfilePictureUrl,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedOn = review.CreatedOn
        };
    }
}
