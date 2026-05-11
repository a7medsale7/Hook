using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories
{
    public interface IMarketplaceListingRequestRepository
    {
        Task<MarketplaceListingRequest?> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<MarketplaceListingRequest>> GetPendingAsync();
        Task<IEnumerable<MarketplaceListingRequest>> GetByUserIdAsync(string userId);
        Task AddAsync(MarketplaceListingRequest request);
        void Update(MarketplaceListingRequest request);
    }
}
