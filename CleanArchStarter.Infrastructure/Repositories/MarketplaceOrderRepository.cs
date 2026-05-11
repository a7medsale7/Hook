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
    public class MarketplaceOrderRepository(ApplicationDbContext context) : IMarketplaceOrderRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<MarketplaceOrder?> GetByIdWithDetailsAsync(Guid id)
        {
            return await context.MarketplaceOrders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)
                .Include(o => o.Buyer)
                .Include(o => o.SellerProfile)
                    .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<IEnumerable<MarketplaceOrder>> GetByBuyerUserIdAsync(string buyerUserId)
        {
            return await context.MarketplaceOrders
                .Where(o => o.BuyerUserId == buyerUserId && !o.IsDeleted)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)
                .Include(o => o.SellerProfile)
                    .ThenInclude(s => s.User)
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<MarketplaceOrder>> GetBySellerProfileIdAsync(Guid sellerProfileId)
        {
            return await context.MarketplaceOrders
                .Where(o => o.SellerProfileId == sellerProfileId && !o.IsDeleted)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)
                .Include(o => o.Buyer)
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<MarketplaceOrder>> GetAllAsync()
        {
            return await context.MarketplaceOrders
                .Where(o => !o.IsDeleted)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Buyer)
                .Include(o => o.SellerProfile)
                    .ThenInclude(s => s.User)
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();
        }

        public async Task AddAsync(MarketplaceOrder order)
        {
            await context.MarketplaceOrders.AddAsync(order);
        }

        public void Update(MarketplaceOrder order)
        {
            context.MarketplaceOrders.Update(order);
        }
    }


}
