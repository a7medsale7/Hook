using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface ITripDateRepository
{
    Task<TripDate?> GetByIdAsync(Guid id);
    Task<IEnumerable<TripDate>> GetByTripIdAsync(Guid tripId);
    Task<IEnumerable<TripDate>> GetByDateRangeAsync(DateTime start, DateTime end);
    Task AddAsync(TripDate tripDate);
    void Update(TripDate tripDate);
    void Delete(TripDate tripDate);
    void HardDelete(TripDate tripDate);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateExpiredDatesAsync(System.Threading.CancellationToken cancellationToken = default);
}
