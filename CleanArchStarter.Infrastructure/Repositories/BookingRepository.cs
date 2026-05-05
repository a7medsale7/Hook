using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class BookingRepository(ApplicationDbContext context) : IBookingRepository
{
    public async Task<Booking?> GetByIdWithDetailsAsync(Guid id)
    {
        return await context.Bookings
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Boat)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Images)
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
    }

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(string userId)
    {
        return await context.Bookings
            .Where(b => b.UserId == userId && !b.IsDeleted)
            .Include(b => b.Payment)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Boat)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Images)
            .OrderByDescending(b => b.CreatedOn)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByTripDateIdAsync(Guid tripDateId)
    {
        return await context.Bookings
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Images)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Boat)
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Where(b => b.TripDateId == tripDateId && !b.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByOwnerIdAsync(Guid ownerProfileId)
    {
        return await context.Bookings
            .Where(b => b.TripDate.Trip.TripManagerId == ownerProfileId && !b.IsDeleted)
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Boat)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Images)
            .OrderByDescending(b => b.CreatedOn)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetAllWithDetailsAsync()
    {
        return await context.Bookings
            .Where(b => !b.IsDeleted)
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Boat)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Images)
            .OrderByDescending(b => b.CreatedOn)
            .ToListAsync();
    }

    public async Task<bool> ExistsForUserAndDateAsync(string userId, Guid tripDateId)
    {
        return await context.Bookings
            .AnyAsync(b => b.UserId == userId && 
                          b.TripDateId == tripDateId && 
                          !b.IsDeleted && 
                          b.Status != BookingStatus.Cancelled && 
                          b.Status != BookingStatus.Rejected);
    }

    public async Task<IEnumerable<Booking>> GetAllFilteredAsync(
        BookingStatus? status = null, 
        string? location = null, 
        DateTime? date = null, 
        string? userId = null, 
        Guid? ownerId = null)
    {
        var query = context.Bookings
            .Where(b => !b.IsDeleted)
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Boat)
            .Include(b => b.TripDate)
                .ThenInclude(d => d.Trip)
                    .ThenInclude(t => t.Images)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(b => b.Status == status.Value);

        if (!string.IsNullOrEmpty(location))
            query = query.Where(b => b.TripDate.Trip.LocationName.Contains(location));

        if (date.HasValue)
            query = query.Where(b => b.TripDate.StartDate.Date == date.Value.Date);

        if (!string.IsNullOrEmpty(userId))
            query = query.Where(b => b.UserId == userId);

        if (ownerId.HasValue && ownerId.Value != Guid.Empty)
            query = query.Where(b => b.TripDate.Trip.TripManagerId == ownerId.Value);

        return await query.OrderByDescending(b => b.CreatedOn).ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        await context.Bookings.AddAsync(booking);
    }

    public void Update(Booking booking)
    {
        context.Bookings.Update(booking);
    }

    public void Delete(Booking booking)
    {
        booking.IsDeleted = true;
        context.Bookings.Update(booking);
    }

    public void HardDelete(Booking booking)
    {
        context.Bookings.Remove(booking);
    }

    public async Task UpdateCompletedBookingsAsync(System.Threading.CancellationToken cancellationToken = default)
    {
        await context.Bookings
            .Where(b => !b.IsDeleted && b.Status == BookingStatus.Confirmed && b.TripDate.StartDate < DateTime.UtcNow)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.Status, BookingStatus.Completed), cancellationToken);
    }
}
