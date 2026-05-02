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
    public class MarketplaceOrderItemConfiguration : IEntityTypeConfiguration<MarketplaceOrderItem>
    {
        public void Configure(EntityTypeBuilder<MarketplaceOrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.LineTotal).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
