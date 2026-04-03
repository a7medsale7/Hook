using CleanArchStarter.Api;
using CleanArchStarter.Infrastructure.Persistence;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1️⃣ Register all dependencies
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
// 2️⃣ Build the app
// ============================================
var app = builder.Build();

// ============================================
// 3️⃣ Apply EF Core Migrations automatically
// ============================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// ============================================
// 4️⃣ Middleware Pipeline
// ============================================

// Swagger setup (enabled for all environments)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchStarter API V1");
    options.RoutePrefix = string.Empty; // Swagger will be at root: http://localhost:xxxx/
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
    DashboardTitle = "CleanArchStarter Jobs Dashboard"
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

// ============================================
// 5️⃣ Run the app
// ============================================
app.Run();