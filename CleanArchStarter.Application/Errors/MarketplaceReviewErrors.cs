using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class MarketplaceReviewErrors
    {
        public static readonly Error NotEligible = new("Marketplace.Review.NotEligible", "You can only review after you confirm delivery.");
        public static readonly Error AlreadyReviewed = new("Marketplace.Review.AlreadyReviewed", "You already reviewed this product for this order.");
        public static readonly Error InvalidRating = new("Marketplace.Review.InvalidRating", "Rating must be between 1 and 5.");
        public static readonly Error OrderNotFound = new("Marketplace.Review.OrderNotFound", "Order not found.");
        public static readonly Error Forbidden = new("Marketplace.Review.Forbidden", "You do not have permission to perform this action.");
    }
}
