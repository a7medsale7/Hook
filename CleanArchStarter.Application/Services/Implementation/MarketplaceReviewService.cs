using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Reviews;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceReviewService(
     IMarketplaceReviewRepository reviewRepository,
     IMarketplaceOrderRepository orderRepository,
     IUnitOfWork unitOfWork) : IMarketplaceReviewService
    {
        private readonly IMarketplaceReviewRepository _reviewRepository = reviewRepository;
        private readonly IMarketplaceOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<MarketplaceReviewPublicResponse>> CreateAsync(string buyerUserId, CreateMarketplaceReviewRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Rating < 1 || request.Rating > 5)
                return Result.Failure<MarketplaceReviewPublicResponse>(MarketplaceReviewErrors.InvalidRating);

            var order = await _orderRepository.GetByIdWithDetailsAsync(request.OrderId);
            if (order is null)
                return Result.Failure<MarketplaceReviewPublicResponse>(MarketplaceReviewErrors.OrderNotFound);

            if (order.BuyerUserId != buyerUserId)
                return Result.Failure<MarketplaceReviewPublicResponse>(MarketplaceReviewErrors.Forbidden);

            if (order.Status != MarketplaceOrderStatus.DeliveredConfirmedByBuyer)
                return Result.Failure<MarketplaceReviewPublicResponse>(MarketplaceReviewErrors.NotEligible);

            // must have the product in this order
            if (!order.Items.Any(i => i.ProductId == request.ProductId))
                return Result.Failure<MarketplaceReviewPublicResponse>(MarketplaceReviewErrors.Forbidden);

            var existing = await _reviewRepository.GetByBuyerProductOrderAsync(buyerUserId, request.ProductId, request.OrderId);
            if (existing != null)
                return Result.Failure<MarketplaceReviewPublicResponse>(MarketplaceReviewErrors.AlreadyReviewed);

            var review = new MarketplaceReview
            {
                BuyerUserId = buyerUserId,
                ProductId = request.ProductId,
                OrderId = request.OrderId,
                Rating = request.Rating,
                Comment = request.Comment
            };

            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // repository doesn't auto-include buyer after add; map minimal
            return Result.Success(new MarketplaceReviewPublicResponse(
                review.Id,
                review.Rating,
                review.Comment,
                "You",
                null,
                review.CreatedOn
            ));
        }

        public async Task<Result<IEnumerable<MarketplaceReviewPublicResponse>>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var reviews = await _reviewRepository.GetByProductIdAsync(productId);
            return Result.Success(reviews.Select(r =>
                new MarketplaceReviewPublicResponse(
                    r.Id,
                    r.Rating,
                    r.Comment,
                    r.Buyer != null ? $"{r.Buyer.FirstName} {r.Buyer.LastName}".Trim() : "Unknown",
                    r.Buyer?.ProfilePictureUrl,
                    r.CreatedOn
                )));
        }
    }


}
