using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories
{
    public interface IMarketplaceProductRepository
    {
        Task<MarketplaceProduct?> GetByIdAsync(Guid id);
        Task<MarketplaceProduct?> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<MarketplaceProduct>> GetAllActiveAsync();
        Task<IEnumerable<MarketplaceProduct>> GetAllForAdminAsync();
        Task<IEnumerable<MarketplaceProduct>> GetBySellerProfileIdAsync(Guid sellerProfileId);
        Task AddAsync(MarketplaceProduct product);
        void Update(MarketplaceProduct product);
        void Delete(MarketplaceProduct product);
    }
}
