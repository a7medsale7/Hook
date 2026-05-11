using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories
{
    public class MarketplaceReviewRepository(ApplicationDbContext context) : IMarketplaceReviewRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<MarketplaceReview?> GetByIdAsync(Guid id)
        {
            return await context.MarketplaceReviews
                .Include(r => r.Buyer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<MarketplaceReview?> GetByBuyerProductOrderAsync(string buyerUserId, Guid productId, Guid orderId)
        {
            return await context.MarketplaceReviews
                .FirstOrDefaultAsync(r => r.BuyerUserId == buyerUserId && r.ProductId == productId && r.OrderId == orderId && !r.IsDeleted);
        }

        public async Task<IEnumerable<MarketplaceReview>> GetByProductIdAsync(Guid productId)
        {
            return await context.MarketplaceReviews
                .Where(r => r.ProductId == productId && !r.IsDeleted)
                .Include(r => r.Buyer)
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task AddAsync(MarketplaceReview review)
        {
            await context.MarketplaceReviews.AddAsync(review);
        }

        public void Update(MarketplaceReview review)
        {
            context.MarketplaceReviews.Update(review);
        }

        public void Delete(MarketplaceReview review)
        {
            review.IsDeleted = true;
            context.MarketplaceReviews.Update(review);
        }
    }


}
