using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories
{
    public interface IMarketplaceReviewRepository
    {
        Task<MarketplaceReview?> GetByIdAsync(Guid id);
        Task<MarketplaceReview?> GetByBuyerProductOrderAsync(string buyerUserId, Guid productId, Guid orderId);
        Task<IEnumerable<MarketplaceReview>> GetByProductIdAsync(Guid productId);
        Task AddAsync(MarketplaceReview review);
        void Update(MarketplaceReview review);
        void Delete(MarketplaceReview review);
    }
}
