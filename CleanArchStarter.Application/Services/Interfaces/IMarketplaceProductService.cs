using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceProductService
    {
        Task<Result<IEnumerable<MarketplaceProductListItemResponse>>> SearchAsync(MarketplaceProductFilterRequest filter, CancellationToken cancellationToken = default);
        Task<Result<MarketplaceProductDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
