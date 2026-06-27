using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Products;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceProductService(IMarketplaceProductRepository productRepository) : IMarketplaceProductService
    {
        public async Task<Result<IEnumerable<MarketplaceProductListItemResponse>>> SearchAsync(MarketplaceProductFilterRequest filter, CancellationToken cancellationToken = default)
        {
            var products = await productRepository.GetAllActiveAsync();

            if (!string.IsNullOrWhiteSpace(filter.Query))
            {
                var q = filter.Query.Trim();
                products = products.Where(p =>
                    (p.Title != null && p.Title.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Description != null && p.Description.Contains(q, StringComparison.OrdinalIgnoreCase)));
            }

            if (filter.Category.HasValue)
                products = products.Where(p => p.Category == filter.Category.Value);

            if (filter.Condition.HasValue)
                products = products.Where(p => p.Condition == filter.Condition.Value);

            if (filter.MinPrice.HasValue)
                products = products.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= filter.MaxPrice.Value);

            // Basic paging (same style used elsewhere)
            var paged = products
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(ToListItem);

            return Result.Success(paged);
        }

        public async Task<Result<MarketplaceProductDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdWithDetailsAsync(id);
            if (product is null)
                return Result.Failure<MarketplaceProductDetailsResponse>(MarketplaceErrors.ProductNotFound);

            var reviews = product.Reviews
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new MarketplaceReviewResponse(
                    r.Id,
                    r.Rating,
                    r.Comment,
                    r.Buyer != null ? $"{r.Buyer.FirstName} {r.Buyer.LastName}".Trim() : "Unknown",
                    r.Buyer?.ProfilePictureUrl,
                    r.CreatedOn))
                .ToList();

            var avgRating = reviews.Count == 0 ? 0 : (decimal)reviews.Average(r => r.Rating);

            var details = new MarketplaceProductDetailsResponse(
                product.Id,
                product.Title,
                product.Description,
                product.Price,
                product.Condition,
                product.Category,
                product.StockQuantity,
                product.SellerProfile != null && !string.IsNullOrWhiteSpace(product.SellerProfile.SellerName)
                    ? product.SellerProfile.SellerName
                    : "Unknown",
                product.SellerProfileId,
                product.SellerProfile?.StoreImageUrl,
                product.Images.OrderByDescending(i => i.IsMainImage).Select(i => i.ImageUrl).ToList(),
                reviews,
                avgRating,
                reviews.Count);

            return Result.Success(details);
        }

        private static MarketplaceProductListItemResponse ToListItem(MarketplaceProduct p)
        {
            var main = p.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl;
            var avgRating = p.Reviews != null && p.Reviews.Any(r => !r.IsDeleted) 
                            ? (decimal)p.Reviews.Where(r => !r.IsDeleted).Average(r => r.Rating) 
                            : 0;

            return new MarketplaceProductListItemResponse(
                p.Id,
                p.Title ?? string.Empty,
                p.Description ?? string.Empty,
                p.Price,
                p.Condition,
                p.Category,
                p.StockQuantity,
                main,
                Math.Round(avgRating, 1));
        }
    }


}
