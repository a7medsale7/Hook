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
    public class SellerProfileRepository(ApplicationDbContext context) : ISellerProfileRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<bool> HasProfileAsync(string userId)
        {
            return await context.SellerProfiles
                .IgnoreQueryFilters()
                .AnyAsync(p => p.UserId == userId);
        }

        public async Task<SellerProfile?> GetByUserIdAsync(string userId)
        {
            return await context.SellerProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);
        }

        public async Task<SellerProfile?> GetByIdAsync(Guid id)
        {
            return await context.SellerProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<SellerProfile>> GetPendingApplicationsAsync()
        {
            return await context.SellerProfiles
                .IgnoreQueryFilters()
                .Where(p => !p.IsDeleted && p.Status == Hook.Domain.Enums.RequestStatus.Pending)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<SellerProfile>> GetAllAsync()
        {
            return await context.SellerProfiles
                .Where(p => !p.IsDeleted)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<SellerProfile>> GetDeletedAsync()
        {
            return await context.SellerProfiles
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted)
                .Include(p => p.User)
                .OrderByDescending(p => p.UpdatedOn ?? p.CreatedOn)
                .ToListAsync();
        }

        public async Task<SellerProfile?> GetByIdWithDeletedAsync(Guid id)
        {
            return await context.SellerProfiles
                .IgnoreQueryFilters()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(SellerProfile profile)
        {
            await context.SellerProfiles.AddAsync(profile);
        }

        public void Update(SellerProfile profile)
        {
            context.SellerProfiles.Update(profile);
        }

        public void SoftDelete(SellerProfile profile)
        {
            profile.IsDeleted = true;
            context.SellerProfiles.Update(profile);
        }
    }


}
