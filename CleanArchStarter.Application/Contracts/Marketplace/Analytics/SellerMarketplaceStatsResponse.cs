using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Analytics
{
    public record SellerMarketplaceStatsResponse(
    int TotalProducts,
    int ActiveProducts,
    int OutOfStockProducts,
    int TotalOrders,
    int PendingOrders,
    int OutForDeliveryOrders,
    int DeliveredOrders,
    int CancelledOrders,
    decimal Revenue,
    IReadOnlyList<Guid> RecentOrderIds
    );

}
