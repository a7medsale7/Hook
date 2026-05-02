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
    public class MarketplaceListingRequestConfiguration : IEntityTypeConfiguration<MarketplaceListingRequest>
    {
        public void Configure(EntityTypeBuilder<MarketplaceListingRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.UserId).IsRequired().HasMaxLength(450);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(4000);

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.User)
           .WithMany(u => u.MarketplaceListingRequests)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Images)
            .WithOne(i => i.ListingRequest)
            .HasForeignKey(i => i.ListingRequestId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
