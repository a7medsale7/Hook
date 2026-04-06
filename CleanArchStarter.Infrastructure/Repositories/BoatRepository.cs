using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class BoatRepository(ApplicationDbContext context) : IBoatRepository
{
    public async Task<Boat?> GetByIdAsync(Guid id)
    {
        return await context.Boats
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
    }

    public async Task<Boat?> GetByIdWithDetailsAsync(Guid id)
    {
        return await context.Boats
            .Include(b => b.Images)
            .Include(b => b.Trips)
            .Include(b => b.OwnerProfile)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
    }

    public async Task<IEnumerable<Boat>> GetByOwnerIdAsync(Guid ownerProfileId)
    {
        return await context.Boats
            .Where(b => b.OwnerProfileId == ownerProfileId && !b.IsDeleted)
            .Include(b => b.Images)
            .Include(b => b.OwnerProfile)
                .ThenInclude(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Boat>> GetAllAsync()
    {
        return await context.Boats
            .Where(b => !b.IsDeleted)
            .Include(b => b.Images)
            .Include(b => b.OwnerProfile)
                .ThenInclude(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Boat>> GetDeletedAsync()
    {
        return await context.Boats
            .IgnoreQueryFilters()
            .Where(b => b.IsDeleted)
            .ToListAsync();
    }

    public async Task AddAsync(Boat boat)
    {
        await context.Boats.AddAsync(boat);
    }

    public void Update(Boat boat)
    {
        context.Boats.Update(boat);
    }

    public void Delete(Boat boat)
    {
        boat.IsDeleted = true;
        context.Boats.Update(boat);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Boats.AnyAsync(b => b.Id == id && !b.IsDeleted);
    }
}
