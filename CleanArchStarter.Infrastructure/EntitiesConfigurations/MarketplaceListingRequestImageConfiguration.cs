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
    public class MarketplaceListingRequestImageConfiguration : IEntityTypeConfiguration<MarketplaceListingRequestImage>
    {
        public void Configure(EntityTypeBuilder<MarketplaceListingRequestImage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(2048);
        }
    }
}
