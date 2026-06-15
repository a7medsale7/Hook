using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class ComplaintSupportConfiguration : IEntityTypeConfiguration<ComplaintSupport>
{
    public void Configure(EntityTypeBuilder<ComplaintSupport> builder)
    {
        builder.HasKey(x => new { x.ComplaintId, x.UserId });

        builder.HasOne(x => x.Complaint)
            .WithMany(c => c.Supports)
            .HasForeignKey(x => x.ComplaintId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(u => u.ComplaintSupports)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
