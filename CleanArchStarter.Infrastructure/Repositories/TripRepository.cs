using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class TripRepository(ApplicationDbContext context) : ITripRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<Trip?> GetByIdAsync(Guid id)
    {
        return await context.Trips
            .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
    }

    public async Task<Trip?> GetByIdWithDetailsAsync(Guid id)
    {
        return await context.Trips
            .Include(t => t.Images)
            .Include(t => t.TripDates.Where(d => !d.IsDeleted))
            .Include(t => t.Boat)
                .ThenInclude(b => b.Images)
            .Include(t => t.TripManager)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
    }

    public async Task<IEnumerable<Trip>> GetAllAsync()
    {
        return await context.Trips
            .Where(t => !t.IsDeleted)
            .Include(t => t.Images)
            .Include(t => t.Boat)
                .ThenInclude(b => b.Images)
            .Include(t => t.TripManager)
                .ThenInclude(p => p.User)
            .Include(t => t.TripDates.Where(d => !d.IsDeleted))
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetByOwnerIdAsync(Guid ownerProfileId)
    {
        return await context.Trips
            .Where(t => t.TripManagerId == ownerProfileId && !t.IsDeleted)
            .Include(t => t.Images)
            .Include(t => t.Boat)
                .ThenInclude(b => b.Images)
            .Include(t => t.TripDates.Where(d => !d.IsDeleted))
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetAvailableTripsAsync()
    {
        return await context.Trips
            .Where(t => !t.IsDeleted && t.TripDates.Any(d => !d.IsDeleted && d.StartDate >= DateTime.UtcNow && d.IsActive && d.AvailableSeats > 0))
            .Include(t => t.Images)
            .Include(t => t.Boat)
                .ThenInclude(b => b.Images)
            .Include(t => t.TripManager)
                .ThenInclude(p => p.User)
            .Include(t => t.TripDates.Where(d => !d.IsDeleted && d.StartDate >= DateTime.UtcNow && d.IsActive && d.AvailableSeats > 0))
            .ToListAsync();
    }

    public async Task AddAsync(Trip trip)
    {
        await context.Trips.AddAsync(trip);
    }

    public async Task AddImageAsync(TripImage image)
    {
        await context.TripImages.AddAsync(image);
    }

    public void Update(Trip trip)
    {
        context.Trips.Update(trip);
    }

    public void Delete(Trip trip)
    {
        trip.IsDeleted = true;
        context.Trips.Update(trip);
    }
}
