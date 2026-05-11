using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Analytics
{
    public record AdminMarketplaceStatsResponse(
    int TotalSellers,
    int PendingListingRequests,
    int TotalProducts,
    int TotalOrders,
    int PendingOrders,
    int OutForDeliveryOrders,
    int DeliveredOrders,
    int CancelledOrders,
    decimal TotalRevenue,
    IReadOnlyList<Guid> RecentOrderIds);

}
