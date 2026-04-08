using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id);
    Task<Payment?> GetByBookingIdAsync(Guid bookingId);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task AddAsync(Payment payment);
    void Update(Payment payment);
}
