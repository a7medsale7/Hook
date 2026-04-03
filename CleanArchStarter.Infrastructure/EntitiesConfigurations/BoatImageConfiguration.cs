using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class BoatImageConfiguration : IEntityTypeConfiguration<BoatImage>
{
    public void Configure(EntityTypeBuilder<BoatImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);
    }
}
