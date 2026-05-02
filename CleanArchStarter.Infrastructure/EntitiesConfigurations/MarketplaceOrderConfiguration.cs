using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.EntitiesConfigurations
{
    public class MarketplaceOrderConfiguration : IEntityTypeConfiguration<MarketplaceOrder>
    {
        public void Configure(EntityTypeBuilder<MarketplaceOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BuyerUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(x => x.Buyer)
                .WithMany(u => u.MarketplaceOrders)
                .HasForeignKey(x => x.BuyerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Total).HasColumnType("decimal(18,2)");

            builder.Property(x => x.ContactEmail).HasMaxLength(256);
            builder.Property(x => x.ContactPhone).HasMaxLength(50);
            builder.Property(x => x.Governorate).HasMaxLength(100);
            builder.Property(x => x.City).HasMaxLength(100);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(400);
            builder.Property(x => x.PostalCode).HasMaxLength(30);

            builder.Property(x => x.CancellationReason).HasMaxLength(1000);

            builder.HasMany(x => x.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
