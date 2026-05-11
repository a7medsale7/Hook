using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories
{
    public interface IMarketplaceCartRepository
    {
        Task<IEnumerable<MarketplaceCartItem>> GetByBuyerUserIdAsync(string buyerUserId);
        Task<MarketplaceCartItem?> GetByBuyerAndProductAsync(string buyerUserId, Guid productId);
        Task AddAsync(MarketplaceCartItem item);
        void Update(MarketplaceCartItem item);
        void Delete(MarketplaceCartItem item);
        Task ClearAsync(string buyerUserId);
    }
}
