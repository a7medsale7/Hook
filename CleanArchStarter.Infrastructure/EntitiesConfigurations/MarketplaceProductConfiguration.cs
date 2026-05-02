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
    public class MarketplaceProductConfiguration : IEntityTypeConfiguration<MarketplaceProduct>
    {
        public void Configure(EntityTypeBuilder<MarketplaceProduct> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.HasMany(x => x.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
