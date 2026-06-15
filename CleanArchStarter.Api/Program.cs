using Hook.Infrastructure.Persistence;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Hook.Api;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1?? Register all dependencies
// ============================================
builder.Services.AddApiDependencies(builder.Configuration);

// Register Hangfire background job server
builder.Services.AddHangfireServer();

// Configure Serilog for logging
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Configure CORS policy (Allow all for now)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ============================================
// 2?? Build the app
// ============================================
var app = builder.Build();

// ============================================
// 3?? Apply EF Core Migrations automatically & Seed Data
// ============================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        await Hook.Infrastructure.Persistence.DatabaseSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

// ============================================
// 4?? Middleware Pipeline
// ============================================

// Swagger setup (enabled for all environments)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fishing Platform API V1");
    options.RoutePrefix = string.Empty;
});

// Hangfire Dashboard setup
app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:UserName"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
        }
    },
    DashboardTitle = "Hook Jobs Dashboard"
});

// Enable CORS
app.UseCors("AllowAll");

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files (wwwroot)
app.UseStaticFiles();

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Global Exception Handler
app.UseExceptionHandler("/error"); // Make sure you have a route/controller to handle errors

// Map API controllers
app.MapControllers();

// Map SignalR Hubs
app.MapHub<Hook.Api.Hubs.NotificationHub>("/notificationHub");

// ============================================
// 4.5?? Schedule Recurring Background Jobs
// ============================================
using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    
    // Check for unpaid bookings every hour
    recurringJobManager.AddOrUpdate<Hook.Infrastructure.BackgroundJobs.UnpaidBookingsCleanupJob>(
        "unpaid-bookings-cleanup",
        job => job.ExecuteAsync(),
        Cron.Hourly);

    // Close past confirmed trips every day at midnight
    recurringJobManager.AddOrUpdate<Hook.Infrastructure.BackgroundJobs.PastTripsCompletionJob>(
        "past-trips-completion",
        job => job.ExecuteAsync(),
        Cron.Daily);

    // Close expired community events every hour
    recurringJobManager.AddOrUpdate<Hook.Infrastructure.BackgroundJobs.ExpiredEventsCleanupJob>(
        "expired-events-cleanup",
        job => job.ExecuteAsync(),
        Cron.Hourly);
}

// ============================================
// 5?? Run the app
// ============================================
app.Run();