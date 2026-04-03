using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class BoatConfiguration : IEntityTypeConfiguration<Boat>
{
    public void Configure(EntityTypeBuilder<Boat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.HasMany(x => x.Images)
            .WithOne(i => i.Boat)
            .HasForeignKey(i => i.BoatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
