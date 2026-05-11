using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Analytics;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceAnalyticsService(
     ISellerProfileRepository sellerProfileRepository,
     IMarketplaceProductRepository productRepository,
     IMarketplaceOrderRepository orderRepository,
     IMarketplaceListingRequestRepository listingRequestRepository) : IMarketplaceAnalyticsService
    {
        public async Task<Result<SellerMarketplaceStatsResponse>> GetSellerStatsAsync(string sellerUserId, CancellationToken cancellationToken = default)
        {
            var seller = await sellerProfileRepository.GetByUserIdAsync(sellerUserId);
            if (seller is null || seller.Status != RequestStatus.Approved)
                return Result.Failure<SellerMarketplaceStatsResponse>(MarketplaceOrderErrors.Forbidden);

            var products = await productRepository.GetBySellerProfileIdAsync(seller.Id);
            var orders = await orderRepository.GetBySellerProfileIdAsync(seller.Id);

            var totalProducts = products.Count();
            var activeProducts = products.Count(p => p.IsActive);
            var outOfStock = products.Count(p => p.StockQuantity <= 0);

            var totalOrders = orders.Count();
            var pending = orders.Count(o => o.Status == MarketplaceOrderStatus.Pending);
            var outForDelivery = orders.Count(o => o.Status == MarketplaceOrderStatus.OutForDelivery);
            var delivered = orders.Count(o => o.Status == MarketplaceOrderStatus.DeliveredConfirmedByBuyer);
            var cancelled = orders.Count(o => o.Status == MarketplaceOrderStatus.Cancelled);

            var revenue = orders
                .Where(o => o.Status == MarketplaceOrderStatus.DeliveredConfirmedByBuyer)
                .Sum(o => o.Total);

            var recentIds = orders.Take(10).Select(o => o.Id).ToList();

            return Result.Success(new SellerMarketplaceStatsResponse(
                totalProducts,
                activeProducts,
                outOfStock,
                totalOrders,
                pending,
                outForDelivery,
                delivered,
                cancelled,
                revenue,
                recentIds
            ));
        }

        public async Task<Result<AdminMarketplaceStatsResponse>> GetAdminStatsAsync(CancellationToken cancellationToken = default)
        {
            var allSellers = await sellerProfileRepository.GetAllAsync();
            var pendingListing = await listingRequestRepository.GetPendingAsync();
            var orders = await orderRepository.GetAllAsync();

            // total products across sellers (approx by summing per seller)
            var totalProducts = 0;
            foreach (var s in allSellers)
            {
                var sellerProducts = await productRepository.GetBySellerProfileIdAsync(s.Id);
                totalProducts += sellerProducts.Count();
            }

            var totalOrders = orders.Count();
            var pending = orders.Count(o => o.Status == MarketplaceOrderStatus.Pending);
            var outForDelivery = orders.Count(o => o.Status == MarketplaceOrderStatus.OutForDelivery);
            var delivered = orders.Count(o => o.Status == MarketplaceOrderStatus.DeliveredConfirmedByBuyer);
            var cancelled = orders.Count(o => o.Status == MarketplaceOrderStatus.Cancelled);
            var revenue = orders.Where(o => o.Status == MarketplaceOrderStatus.DeliveredConfirmedByBuyer).Sum(o => o.Total);

            var recentIds = orders.Take(10).Select(o => o.Id).ToList();

            return Result.Success(new AdminMarketplaceStatsResponse(
                TotalSellers: allSellers.Count(),
                PendingListingRequests: pendingListing.Count(),
                TotalProducts: totalProducts,
                TotalOrders: totalOrders,
                PendingOrders: pending,
                OutForDeliveryOrders: outForDelivery,
                DeliveredOrders: delivered,
                CancelledOrders: cancelled,
                TotalRevenue: revenue,
                RecentOrderIds: recentIds
            ));
        }
    }


}
