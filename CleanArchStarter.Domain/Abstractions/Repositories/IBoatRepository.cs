using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface IBoatRepository
{
    Task<Boat?> GetByIdAsync(Guid id);
    Task<Boat?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Boat>> GetByOwnerIdAsync(Guid ownerProfileId);
    Task<IEnumerable<Boat>> GetAllAsync();
    Task<IEnumerable<Boat>> GetDeletedAsync();
    Task AddAsync(Boat boat);
    Task AddImageAsync(BoatImage image);
    void Update(Boat boat);
    void Delete(Boat boat);
    Task<bool> ExistsAsync(Guid id);
}
