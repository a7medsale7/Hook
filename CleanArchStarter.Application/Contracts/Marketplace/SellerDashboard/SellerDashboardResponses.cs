using System;

namespace Hook.Application.Contracts.Marketplace.SellerDashboard
{
    public record SellerDashboardStatsResponse(
        int TotalOrders,
        int ActiveProducts,
        int TotalProducts,
        decimal TotalRevenue,
        decimal MonthlyRevenue,
        decimal AverageOrderValue
    );

    public record SellerRecentOrderResponse(
        Guid OrderId,
        string BuyerName,
        decimal Total,
        string Status,
        DateTime Date
    );

    public record SellerRecentReviewResponse(
        Guid ReviewId,
        string ProductTitle,
        string BuyerName,
        int Rating,
        string? Comment,
        DateTime Date
    );

    public record SellerMonthlySalesResponse(
        int Year,
        int Month,
        decimal Revenue,
        int OrdersCount
    );

    public record SellerTopProductResponse(
        Guid ProductId,
        string ProductName,
        int QuantitySold,
        decimal Profit,
        double RevenuePercentage
    );
}
