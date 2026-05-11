using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceSellerProductService
    {
        Task<Result<Guid>> CreateAsync(string userId, CreateMarketplaceProductRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(string userId, UpdateMarketplaceProductRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(string userId, Guid productId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<MarketplaceProductListItemResponse>>> GetMyProductsAsync(string userId, CancellationToken cancellationToken = default);
    }
}
