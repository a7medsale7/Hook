using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.ShortDescription)
            .HasMaxLength(500);

        builder.Property(x => x.PricePerPerson)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Boat)
            .WithMany(b => b.Trips)
            .HasForeignKey(x => x.BoatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Images)
            .WithOne(i => i.Trip)
            .HasForeignKey(i => i.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TripDates)
            .WithOne(td => td.Trip)
            .HasForeignKey(td => td.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Reviews)
            .WithOne(r => r.Trip)
            .HasForeignKey(r => r.TripId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
