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
    public class SellerProfileConfiguration : IEntityTypeConfiguration<SellerProfile>
    {
        public void Configure(EntityTypeBuilder<SellerProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.SellerName)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(x => x.Governorate)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(x => x.NationalIdPhotoUrl)
                .IsRequired()
                .HasMaxLength(2048);

            builder.HasOne(x => x.User)
                .WithOne(u => u.SellerProfile)
                .HasForeignKey<SellerProfile>(x => x.UserId);

            builder.HasMany(x => x.Products)
                .WithOne(p => p.SellerProfile)
                .HasForeignKey(p => p.SellerProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x=>x.Orders)
                .WithOne(o=>o.SellerProfile)
                .HasForeignKey(o=>o.SellerProfileId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
