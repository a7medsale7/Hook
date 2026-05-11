using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories
{
    public interface IMarketplaceOrderRepository
    {
        Task<MarketplaceOrder?> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<MarketplaceOrder>> GetByBuyerUserIdAsync(string buyerUserId);
        Task<IEnumerable<MarketplaceOrder>> GetBySellerProfileIdAsync(Guid sellerProfileId);
        Task<IEnumerable<MarketplaceOrder>> GetAllAsync();
        Task AddAsync(MarketplaceOrder order);
        void Update(MarketplaceOrder order);
    }
}
