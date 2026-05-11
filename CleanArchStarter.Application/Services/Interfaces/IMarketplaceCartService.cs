using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceCartService
    {
        Task<Result<MarketplaceCartResponse>> GetMyCartAsync(string buyerUserId, CancellationToken cancellationToken = default);
        Task<Result<MarketplaceCartResponse>> AddToCartAsync(string buyerUserId, AddToCartRequest request, CancellationToken cancellationToken = default);
        Task<Result<MarketplaceCartResponse>> UpdateQuantityAsync(string buyerUserId, UpdateCartItemQuantityRequest request, CancellationToken cancellationToken = default);
        Task<Result> RemoveAsync(string buyerUserId, Guid productId, CancellationToken cancellationToken = default);
        Task<Result> ClearAsync(string buyerUserId, CancellationToken cancellationToken = default);
    }
}
