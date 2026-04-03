using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class TripImageConfiguration : IEntityTypeConfiguration<TripImage>
{
    public void Configure(EntityTypeBuilder<TripImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);
    }
}
