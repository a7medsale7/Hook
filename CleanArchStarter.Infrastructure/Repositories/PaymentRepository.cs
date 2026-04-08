using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class PaymentRepository(ApplicationDbContext context) : IPaymentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        return await _context.Payments
            .Include(p => p.Booking)
                .ThenInclude(b => b.TripDate)
                    .ThenInclude(d => d.Trip)
            .Include(p => p.Booking)
                .ThenInclude(b => b.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Payment?> GetByBookingIdAsync(Guid bookingId)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(p => p.BookingId == bookingId);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .Include(p => p.Booking)
                .ThenInclude(b => b.TripDate)
                    .ThenInclude(d => d.Trip)
            .Include(p => p.Booking)
                .ThenInclude(b => b.User)
            .ToListAsync();
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public void Update(Payment payment)
    {
        _context.Payments.Update(payment);
    }
}
