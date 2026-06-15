using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class FishingEventConfiguration : IEntityTypeConfiguration<FishingEvent>
{
    public void Configure(EntityTypeBuilder<FishingEvent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Post)
            .WithOne(p => p.EventDetails)
            .HasForeignKey<FishingEvent>(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
