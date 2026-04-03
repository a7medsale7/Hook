using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class TripDateConfiguration : IEntityTypeConfiguration<TripDate>
{
    public void Configure(EntityTypeBuilder<TripDate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Bookings)
            .WithOne(b => b.TripDate)
            .HasForeignKey(b => b.TripDateId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ قاعدة التحقق: تاريخ الانتهاء يجب أن يكون بعد تاريخ البداية
        builder.ToTable(t => t.HasCheckConstraint("CK_TripDate_Dates", "EndDate > StartDate"));
    }
}
