using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TransactionId)
            .HasMaxLength(150);

        builder.Property(x => x.ReceiptImageUrl)
            .HasMaxLength(500);

        builder.Property(x => x.AdminNotes)
            .HasMaxLength(500);
    }
}
