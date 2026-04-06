using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface ITripRepository
{
    Task<Trip?> GetByIdAsync(Guid id);
    Task<Trip?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Trip>> GetAllAsync();
    Task<IEnumerable<Trip>> GetByOwnerIdAsync(Guid ownerProfileId);
    Task<IEnumerable<Trip>> GetAvailableTripsAsync(); // Trips with upcoming dates
    Task AddAsync(Trip trip);
    void Update(Trip trip);
    void Delete(Trip trip);
}
