using Hangfire;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Infrastructure.BackgroundJobs;

public class PastTripsCompletionJob(
    IServiceScopeFactory scopeFactory,
    ILogger<PastTripsCompletionJob> logger)
{
    public async Task ExecuteAsync()
    {
        logger.LogInformation("Starting Past Trips Completion Job at: {time}", DateTimeOffset.UtcNow);

        try
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Find trips that ended in the past
            // And have bookings that are Confirmed but not yet Completed
            var now = DateTime.UtcNow;

            var confirmedBookingsToComplete = await context.Bookings
                .Include(b => b.User)
                .Include(b => b.TripDate)
                    .ThenInclude(d => d.Trip)
                .Where(b => b.Status == BookingStatus.Confirmed 
                         && b.TripDate != null 
                         && b.TripDate.EndDate < now
                         && !b.IsDeleted)
                .ToListAsync();

            var count = 0;
            var backgroundJobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

            foreach (var booking in confirmedBookingsToComplete)
            {
                booking.Status = BookingStatus.Completed;
                
                // Enqueue an email asking for a review
                if (booking.User?.Email != null && booking.TripDate?.Trip != null)
                {
                    string userName = $"{booking.User.FirstName} {booking.User.LastName}";
                    // Frontend route for reviewing a specific trip or booking
                    string reviewUrl = $"https://hook.com/dashboard/trips/{booking.TripDate.Trip.Id}/review?bookingId={booking.Id}";
                    string html = Hook.Domain.Helpers.EmailTemplates.GetTripReviewRequestTemplate(userName, booking.TripDate.Trip.Title, reviewUrl);
                    
                    backgroundJobClient.Enqueue<IEmailSender>(sender => 
                        sender.SendEmailAsync(booking.User.Email, "🎣 How was your trip? Leave a review!", html));
                }

                count++;
            }

            if (count > 0)
            {
                await context.SaveChangesAsync();
                logger.LogInformation($"Successfully completed {count} bookings for past trips.");
            }
            else
            {
                logger.LogInformation("No past confirmed bookings found to complete.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing PastTripsCompletionJob.");
            throw;
        }
    }
}
