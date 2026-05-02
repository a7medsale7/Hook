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
    public class MarketplaceReviewConfiguration : IEntityTypeConfiguration<MarketplaceReview>
    {
        public void Configure(EntityTypeBuilder<MarketplaceReview> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Comment)
            .HasMaxLength(2000);

            builder.Property(x => x.BuyerUserId)
            .IsRequired()
            .HasMaxLength(450);

            builder.HasOne(x => x.Buyer)
            .WithMany(u => u.MarketplaceReviews)
            .HasForeignKey(x => x.BuyerUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.BuyerUserId, x.ProductId, x.OrderId })
            .IsUnique();
        }
    }
}
