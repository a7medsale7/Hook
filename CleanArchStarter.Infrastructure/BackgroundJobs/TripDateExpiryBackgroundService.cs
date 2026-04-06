using Hook.Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Infrastructure.BackgroundJobs;

public class TripDateExpiryBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<TripDateExpiryBackgroundService> logger) : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(6); // Run every 6 hours

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Trip Date Expiry Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Running Trip Date Expiry process at: {time}", DateTimeOffset.Now);

                using (var scope = scopeFactory.CreateScope())
                {
                    var tripDateRepository = scope.ServiceProvider.GetRequiredService<ITripDateRepository>();
                    await tripDateRepository.UpdateExpiredDatesAsync(stoppingToken);
                }

                logger.LogInformation("Trip Date Expiry process completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while expiring trip dates.");
            }

            // Wait for the next interval
            await Task.Delay(_checkInterval, stoppingToken);
        }

        logger.LogInformation("Trip Date Expiry Background Service is stopping.");
    }
}
