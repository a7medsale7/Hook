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
    public class MarketplaceCartRepository(ApplicationDbContext context) : IMarketplaceCartRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<IEnumerable<MarketplaceCartItem>> GetByBuyerUserIdAsync(string buyerUserId)
        {
            return await context.MarketplaceCartItems
                .Where(i => i.BuyerUserId == buyerUserId && !i.IsDeleted)
                .Include(i => i.Product)
                    .ThenInclude(p => p.Images)
                .Include(i => i.Product)
                    .ThenInclude(p => p.SellerProfile)
                        .ThenInclude(s => s.User)
                .OrderByDescending(i => i.CreatedOn)
                .ToListAsync();
        }

        public async Task<MarketplaceCartItem?> GetByBuyerAndProductAsync(string buyerUserId, Guid productId)
        {
            return await context.MarketplaceCartItems
                .Include(i => i.Product)
                    .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(i => i.BuyerUserId == buyerUserId && i.ProductId == productId && !i.IsDeleted);
        }

        public async Task AddAsync(MarketplaceCartItem item)
        {
            await context.MarketplaceCartItems.AddAsync(item);
        }

        public void Update(MarketplaceCartItem item)
        {
            context.MarketplaceCartItems.Update(item);
        }

        public void Delete(MarketplaceCartItem item)
        {
            item.IsDeleted = true;
            context.MarketplaceCartItems.Update(item);
        }

        public async Task ClearAsync(string buyerUserId)
        {
            var items = await context.MarketplaceCartItems
                .Where(i => i.BuyerUserId == buyerUserId && !i.IsDeleted)
                .ToListAsync();

            foreach (var item in items)
            {
                item.IsDeleted = true;
            }

            context.MarketplaceCartItems.UpdateRange(items);
        }
    }
}
