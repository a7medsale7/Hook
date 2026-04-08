using Hook.Application.Abstractions.Result;

namespace Hook.Application.Errors;

public static class ReviewErrors
{
    public static readonly Error NotFound = new(
        "Review.NotFound",
        "The requested review was not found.");

    public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "You can only review a trip you have booked and completed.");

    public static readonly Error AlreadyReviewed = new(
        "Review.AlreadyReviewed",
        "You have already reviewed this booking.");

    public static readonly Error InvalidRating = new(
        "Review.InvalidRating",
        "Rating must be between 1 and 5.");
}
