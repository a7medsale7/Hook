using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories
{
    public class MarketplaceListingRequestRepository(ApplicationDbContext context) : IMarketplaceListingRequestRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<MarketplaceListingRequest?> GetByIdWithDetailsAsync(Guid id)
        {
            return await context.MarketplaceListingRequests
                .Include(r => r.Images)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<IEnumerable<MarketplaceListingRequest>> GetPendingAsync()
        {
            return await context.MarketplaceListingRequests
                .Where(r => !r.IsDeleted && r.Status == RequestStatus.Pending)
                .Include(r => r.Images)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<MarketplaceListingRequest>> GetByUserIdAsync(string userId)
        {
            return await context.MarketplaceListingRequests
                .Where(r => !r.IsDeleted && r.UserId == userId)
                .Include(r => r.Images)
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task AddAsync(MarketplaceListingRequest request)
        {
            await context.MarketplaceListingRequests.AddAsync(request);
        }

        public void Update(MarketplaceListingRequest request)
        {
            context.MarketplaceListingRequests.Update(request);
        }
    }


}
