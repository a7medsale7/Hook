using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(450);

        // ✅ ربط اليوزر بالـ Review - لو اتحذف اليوزر لا تحذف المراجعات (Restrict)
        builder.HasOne(x => x.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ الربط بالحجز - إثبات أن من يراجع قد حجز فعلاً
        builder.HasOne(x => x.Booking)
            .WithMany(b => b.Reviews)
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Unique index: كل يوزر يقدر يكتب مراجعة واحدة بس لكل رحلة
        builder.HasIndex(x => new { x.UserId, x.TripId })
            .IsUnique();

        builder.Property(x => x.Comment)
            .HasMaxLength(1000);

        // ✅ قيد قاعدة البيانات: التقييم بين 1 و 5 فقط
        builder.ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5"));
    }
}
