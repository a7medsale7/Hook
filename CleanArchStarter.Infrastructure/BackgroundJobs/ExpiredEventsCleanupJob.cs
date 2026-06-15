using Hangfire;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.BackgroundJobs;

public class ExpiredEventsCleanupJob(
    IServiceScopeFactory scopeFactory,
    ILogger<ExpiredEventsCleanupJob> logger)
{
    public async Task ExecuteAsync()
    {
        logger.LogInformation("Starting Expired Events Cleanup Job at: {time}", DateTimeOffset.UtcNow);

        try
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var now = DateTime.UtcNow;

            // Find events where the EventDate has passed and status is Open or Full
            var expiredEvents = await context.FishingEvents
                .Where(e => e.EventDate < now 
                         && (e.Status == EventStatus.Open || e.Status == EventStatus.Full)
                         && !e.IsDeleted)
                .ToListAsync();

            var count = 0;
            foreach (var ev in expiredEvents)
            {
                ev.Status = EventStatus.Closed;
                count++;
            }

            if (count > 0)
            {
                await context.SaveChangesAsync();
                logger.LogInformation("Successfully closed {count} expired events.", count);
            }
            else
            {
                logger.LogInformation("No expired events found to close.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing ExpiredEventsCleanupJob.");
            throw;
        }
    }
}
