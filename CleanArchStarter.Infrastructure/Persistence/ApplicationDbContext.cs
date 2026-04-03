using Hook.Domain.Entities;
using Hook.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Persistence;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // مهم جداً عشان جداول الـ Identity تتكريت
        // السطر السحري ده بيروح يدور على أي كلاس بيطبق الـ IEntityTypeConfiguration
        // في نفس الـ Assembly دي، ويطبق كل الـ Fluent API بتاعنا مرة واحدة!
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // تطبيق الـ Soft Delete  على مستوى الاستعلامات
        builder.Entity<BoatOwnerProfile>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<Boat>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<BoatImage>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<Trip>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<TripImage>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<TripDate>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<Booking>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<Review>().HasQueryFilter(e => !e.IsDeleted);
    }

    public DbSet<BoatOwnerProfile> BoatOwnerProfiles { get; set; }
    public DbSet<Boat> Boats { get; set; }
    public DbSet<BoatImage> BoatImages { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripImage> TripImages { get; set; }
    public DbSet<TripDate> TripDates { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // جلب الـ UserId من الـ HttpContext
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            userId = "System"; // قيمة افتراضية لو مش موجود مستخدم مسجل دخول
        }

        foreach (var entry in ChangeTracker.Entries<Auditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = userId;
                    entry.Entity.CreatedOn = DateTime.UtcNow; // ضبط تاريخ الإنشاء
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedById = userId;
                    entry.Entity.UpdatedOn = DateTime.UtcNow; // ضبط تاريخ آخر تعديل
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}