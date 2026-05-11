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
    public class MarketplaceProductRepository(ApplicationDbContext context) : IMarketplaceProductRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<MarketplaceProduct?> GetByIdAsync(Guid id)
        {
            return await context.MarketplaceProducts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<MarketplaceProduct?> GetByIdWithDetailsAsync(Guid id)
        {
            return await context.MarketplaceProducts
                .Include(p => p.Images)
                .Include(p => p.SellerProfile)
                    .ThenInclude(s => s.User)
                .Include(p => p.Reviews.Where(r => !r.IsDeleted))
                    .ThenInclude(r => r.Buyer)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted && p.IsActive);
        }

        public async Task<IEnumerable<MarketplaceProduct>> GetAllActiveAsync()
        {
            return await context.MarketplaceProducts
                .Where(p => !p.IsDeleted && p.IsActive)
                .Include(p => p.Images)
                .Include(p => p.SellerProfile)
                    .ThenInclude(s => s.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<MarketplaceProduct>> GetAllForAdminAsync()
        {
            return await context.MarketplaceProducts
                .IgnoreQueryFilters()
                .Where(p => !p.IsDeleted)
                .Include(p => p.Images)
                .Include(p => p.SellerProfile)
                    .ThenInclude(s => s.User)
                .OrderByDescending(p => p.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<MarketplaceProduct>> GetBySellerProfileIdAsync(Guid sellerProfileId)
        {
            return await context.MarketplaceProducts
                .Where(p => !p.IsDeleted && p.SellerProfileId == sellerProfileId)
                .Include(p => p.Images)
                .Include(p => p.Reviews.Where(r => !r.IsDeleted))
                .OrderByDescending(p => p.CreatedOn)
                .ToListAsync();
        }

        public async Task AddAsync(MarketplaceProduct product)
        {
            await context.MarketplaceProducts.AddAsync(product);
        }

        public void Update(MarketplaceProduct product)
        {
            context.MarketplaceProducts.Update(product);
        }

        public void Delete(MarketplaceProduct product)
        {
            product.IsDeleted = true;
            context.MarketplaceProducts.Update(product);
        }
    }


}
