using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceOrderService
    {
        Task<Result<IEnumerable<MarketplaceOrderResponse>>> CreateAsync(string buyerUserId, CreateMarketplaceOrderRequest request, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<MarketplaceOrderResponse>>> GetMyPurchasesAsync(string buyerUserId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<MarketplaceOrderResponse>>> GetMySellerOrdersAsync(string sellerUserId, CancellationToken cancellationToken = default);

        Task<Result<MarketplaceOrderResponse>> MarkOutForDeliveryAsync(Guid orderId, string sellerUserId, CancellationToken cancellationToken = default);
        Task<Result<MarketplaceOrderResponse>> SellerCancelAsync(Guid orderId, string sellerUserId, string reason, CancellationToken cancellationToken = default);

        Task<Result> BuyerCancelAsync(Guid orderId, string buyerUserId, CancellationToken cancellationToken = default);
        Task<Result<MarketplaceOrderResponse>> BuyerConfirmReceivedAsync(Guid orderId, string buyerUserId, CancellationToken cancellationToken = default);
    }
}
 