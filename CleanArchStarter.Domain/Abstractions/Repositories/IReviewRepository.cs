using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<Review?> GetByBookingIdAsync(Guid bookingId);
    Task<IEnumerable<Review>> GetByTripIdAsync(Guid tripId);
    Task<IEnumerable<Review>> GetByUserIdAsync(string userId);
    Task AddAsync(Review review);
    void Update(Review review);
    void Delete(Review review);
}
