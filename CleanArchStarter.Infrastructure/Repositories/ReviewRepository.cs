using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class ReviewRepository(ApplicationDbContext context) : IReviewRepository
{
    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await context.Reviews
            .Include(r => r.User)
            .Include(r => r.Trip)
            .Include(r => r.Booking)
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
    }

    public async Task<Review?> GetByBookingIdAsync(Guid bookingId)
    {
        return await context.Reviews
            .FirstOrDefaultAsync(r => r.BookingId == bookingId && !r.IsDeleted);
    }

    public async Task<IEnumerable<Review>> GetByTripIdAsync(Guid tripId)
    {
        return await context.Reviews
            .Include(r => r.User)
            .Include(r => r.Trip)
            .Where(r => r.TripId == tripId && !r.IsDeleted)
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByUserIdAsync(string userId)
    {
        return await context.Reviews
            .Include(r => r.Trip)
            .Include(r => r.User)
            .Where(r => r.UserId == userId && !r.IsDeleted)
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByOwnerIdAsync(Guid ownerId)
    {
        return await context.Reviews
            .Where(r => r.Trip.TripManagerId == ownerId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task AddAsync(Review review)
    {
        await context.Reviews.AddAsync(review);
    }

    public void Update(Review review)
    {
        context.Reviews.Update(review);
    }

    public void Delete(Review review)
    {
        review.IsDeleted = true;
        context.Reviews.Update(review);
    }
}
