using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Infrastructure.BackgroundJobs;

public class UnpaidBookingsCleanupJob(
    IServiceScopeFactory scopeFactory,
    ILogger<UnpaidBookingsCleanupJob> logger)
{
    public async Task ExecuteAsync()
    {
        logger.LogInformation("Starting Unpaid Bookings Cleanup Job at: {time}", DateTimeOffset.UtcNow);

        try
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var thresholdTime = DateTime.UtcNow.AddHours(-24);

            var unpaidBookings = await context.Bookings
                .Include(b => b.Payment)
                .Include(b => b.TripDate)
                .Where(b => b.Status == BookingStatus.Pending 
                         && b.CreatedOn < thresholdTime 
                         && !b.IsDeleted)
                .ToListAsync();

            var count = 0;

            foreach (var booking in unpaidBookings)
            {
                // We mainly care about InstaPay where receipt wasn't uploaded
                // Or Online where payment didn't go through
                // For Cash, it might be pending until the day, so we skip auto-cancel for Cash.
                if (booking.Payment != null 
                    && booking.Payment.PaymentMethod != PaymentMethod.Cash 
                    && string.IsNullOrEmpty(booking.Payment.ReceiptImageUrl)
                    && booking.Payment.Status == PaymentStatus.Pending)
                {
                    booking.Status = BookingStatus.Cancelled;
                    booking.Payment.Status = PaymentStatus.Rejected;
                    booking.Payment.AdminNotes = "Auto-cancelled due to lack of payment receipt within 24 hours.";

                    // Restore available seats
                    if (booking.TripDate != null)
                    {
                        booking.TripDate.AvailableSeats += booking.NumberOfParticipants;
                    }

                    count++;
                }
            }

            if (count > 0)
            {
                await context.SaveChangesAsync();
                logger.LogInformation($"Successfully cancelled {count} unpaid bookings.");
            }
            else
            {
                logger.LogInformation("No unpaid bookings found to cancel.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing UnpaidBookingsCleanupJob.");
            throw; // Rethrow to let Hangfire know the job failed
        }
    }
}
