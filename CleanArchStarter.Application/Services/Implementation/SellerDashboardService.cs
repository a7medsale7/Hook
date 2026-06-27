using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.SellerDashboard;
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
    public class SellerDashboardService(
        ISellerProfileRepository sellerProfileRepository,
        IMarketplaceOrderRepository orderRepository,
        IMarketplaceProductRepository productRepository,
        IMarketplaceReviewRepository reviewRepository) : ISellerDashboardService
    {
        private readonly ISellerProfileRepository _sellerProfileRepository = sellerProfileRepository;
        private readonly IMarketplaceOrderRepository _orderRepository = orderRepository;
        private readonly IMarketplaceProductRepository _productRepository = productRepository;
        private readonly IMarketplaceReviewRepository _reviewRepository = reviewRepository;

        public async Task<Result<SellerDashboardStatsResponse>> GetStatisticsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<SellerDashboardStatsResponse>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var orders = await _orderRepository.GetBySellerProfileIdAsync(sellerProfile.Id);
            var products = await _productRepository.GetBySellerProfileIdAsync(sellerProfile.Id);

            var totalOrders = orders.Count();
            var totalProducts = products.Count();
            var activeProducts = products.Count(p => p.IsActive);
            
            var validOrders = orders.Where(o => o.Status != MarketplaceOrderStatus.Cancelled);
            var totalRevenue = validOrders.Sum(o => o.Total);
            var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;
            var monthlyRevenue = validOrders
                .Where(o => o.CreatedOn.Month == currentMonth && o.CreatedOn.Year == currentYear)
                .Sum(o => o.Total);

            return Result.Success(new SellerDashboardStatsResponse(
                totalOrders,
                activeProducts,
                totalProducts,
                totalRevenue,
                monthlyRevenue,
                averageOrderValue
            ));
        }

        public async Task<Result<IEnumerable<SellerRecentOrderResponse>>> GetRecentOrdersAsync(string userId, int count = 20, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<SellerRecentOrderResponse>>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var orders = await _orderRepository.GetBySellerProfileIdAsync(sellerProfile.Id);
            var recentOrders = orders
                .OrderByDescending(o => o.CreatedOn)
                .Take(count)
                .Select(o => new SellerRecentOrderResponse(
                    o.Id,
                    $"{o.FirstName} {o.LastName}".Trim(),
                    o.Total,
                    o.Status.ToString(),
                    o.CreatedOn
                ));

            return Result.Success(recentOrders);
        }

        public async Task<Result<IEnumerable<SellerRecentReviewResponse>>> GetRecentReviewsAsync(string userId, int count = 20, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<SellerRecentReviewResponse>>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var reviews = await _reviewRepository.GetBySellerProfileIdAsync(sellerProfile.Id);
            var recentReviews = reviews
                .Take(count)
                .Select(r => new SellerRecentReviewResponse(
                    r.Id,
                    r.Product.Title,
                    r.Buyer?.FirstName + " " + r.Buyer?.LastName,
                    r.Rating,
                    r.Comment,
                    r.CreatedOn
                ));

            return Result.Success(recentReviews);
        }

        public async Task<Result<IEnumerable<SellerMonthlySalesResponse>>> GetSalesOverTimeAsync(string userId, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<SellerMonthlySalesResponse>>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var orders = await _orderRepository.GetBySellerProfileIdAsync(sellerProfile.Id);
            
            var validOrders = orders.Where(o => o.Status != MarketplaceOrderStatus.Cancelled);
            
            var salesOverTime = validOrders
                .GroupBy(o => new { o.CreatedOn.Year, o.CreatedOn.Month })
                .Select(g => new SellerMonthlySalesResponse(
                    g.Key.Year,
                    g.Key.Month,
                    g.Sum(o => o.Total),
                    g.Count()
                ))
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .Take(12) // Last 12 months
                .ToList()
                // Reverse to be chronological (oldest to newest)
                .OrderBy(s => s.Year)
                .ThenBy(s => s.Month)
                .ToList();

            return Result.Success((IEnumerable<SellerMonthlySalesResponse>)salesOverTime);
        }
        public async Task<Result<IEnumerable<SellerTopProductResponse>>> GetTopSellingProductsAsync(string userId, int count = 5, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<SellerTopProductResponse>>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var orders = await _orderRepository.GetBySellerProfileIdAsync(sellerProfile.Id);
            var validOrders = orders.Where(o => o.Status != MarketplaceOrderStatus.Cancelled).ToList();
            
            var totalRevenue = validOrders.Sum(o => o.Total);

            var allItems = validOrders.SelectMany(o => o.Items);
            
            var topProducts = allItems
                .GroupBy(i => new { i.ProductId, i.Product.Title })
                .Select(g => {
                    var productProfit = g.Sum(i => i.LineTotal);
                    var revenuePercentage = totalRevenue > 0 ? (double)(productProfit / totalRevenue) * 100 : 0;
                    
                    return new SellerTopProductResponse(
                        g.Key.ProductId,
                        g.Key.Title,
                        g.Sum(i => i.Quantity),
                        productProfit,
                        Math.Round(revenuePercentage, 2)
                    );
                })
                .OrderByDescending(p => p.QuantitySold)
                .Take(count)
                .ToList();

            return Result.Success((IEnumerable<SellerTopProductResponse>)topProducts);
        }
    }
}
