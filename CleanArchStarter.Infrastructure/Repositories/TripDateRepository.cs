using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class TripDateRepository(ApplicationDbContext context) : ITripDateRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<TripDate?> GetByIdAsync(Guid id)
    {
        return await context.TripDates
            .Include(d => d.Trip)
                .ThenInclude(t => t.TripManager)
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
    }

    public async Task<IEnumerable<TripDate>> GetByTripIdAsync(Guid tripId)
    {
        return await context.TripDates
            .Where(d => d.TripId == tripId && !d.IsDeleted)
            .OrderBy(d => d.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<TripDate>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        return await context.TripDates
            .Where(d => !d.IsDeleted && d.StartDate >= start && d.EndDate <= end && d.IsActive)
            .Include(d => d.Trip)
            .OrderBy(d => d.StartDate)
            .ToListAsync();
    }

    public async Task AddAsync(TripDate tripDate)
    {
        await context.TripDates.AddAsync(tripDate);
    }

    public void Update(TripDate tripDate)
    {
        context.TripDates.Update(tripDate);
    }

    public void Delete(TripDate tripDate)
    {
        tripDate.IsDeleted = true;
        context.TripDates.Update(tripDate);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.TripDates.AnyAsync(d => d.Id == id && !d.IsDeleted);
    }

    public async Task UpdateExpiredDatesAsync(System.Threading.CancellationToken cancellationToken = default)
    {
        await context.TripDates
            .Where(d => d.IsActive && d.StartDate < DateTime.UtcNow)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsActive, false), cancellationToken);
    }
}
