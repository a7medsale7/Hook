using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Admin;
using Hook.Application.Contracts.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceAdminManagementService
    {
        Task<Result<IEnumerable<SellerResponse>>> GetAllSellersAsync(CancellationToken cancellationToken = default);
        Task<Result> DeleteSellerAsync(Guid sellerProfileId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<AdminMarketplaceProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<Result> DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
