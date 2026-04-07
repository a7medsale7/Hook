using Hook.Domain.Entities;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Booking>> GetByTripDateIdAsync(Guid tripDateId);
    Task<IEnumerable<Booking>> GetByOwnerIdAsync(Guid ownerProfileId);
    Task<bool> ExistsForUserAndDateAsync(string userId, Guid tripDateId);
    Task<IEnumerable<Booking>> GetAllWithDetailsAsync();
    Task<IEnumerable<Booking>> GetAllFilteredAsync(
        BookingStatus? status = null, 
        string? location = null, 
        DateTime? date = null, 
        string? userId = null, 
        Guid? ownerId = null);
    Task AddAsync(Booking booking);
    void Update(Booking booking);
    void Delete(Booking booking);
}
