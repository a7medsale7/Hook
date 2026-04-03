using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class BoatOwnerProfileConfiguration : IEntityTypeConfiguration<BoatOwnerProfile>
{
    public void Configure(EntityTypeBuilder<BoatOwnerProfile> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.HasOne(x => x.User)
            .WithOne(u => u.BoatOwnerProfile)
            .HasForeignKey<BoatOwnerProfile>(x => x.UserId);

        builder.Property(x => x.NationalIdNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BoatLicenseNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.Boats)
            .WithOne(b => b.OwnerProfile)
            .HasForeignKey(b => b.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ManagedTrips)
            .WithOne(t => t.TripManager)
            .HasForeignKey(t => t.TripManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
