using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(450);

        // ✅ لو اتحذف اليوزر لا تحذف حجوزاته (تاريخ مالي مهم)
        builder.HasOne(x => x.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(b => b.Payment)
            .WithOne(p => p.Booking)
            .HasForeignKey<Payment>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
