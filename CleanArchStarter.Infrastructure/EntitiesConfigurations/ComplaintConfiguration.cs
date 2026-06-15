using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
{
    public void Configure(EntityTypeBuilder<Complaint> builder)
    {
        builder.HasKey(x => x.PostId);

        builder.HasOne(x => x.Post)
            .WithOne(p => p.ComplaintDetails)
            .HasForeignKey<Complaint>(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.AdminNotes)
            .HasMaxLength(1000);
    }
}
