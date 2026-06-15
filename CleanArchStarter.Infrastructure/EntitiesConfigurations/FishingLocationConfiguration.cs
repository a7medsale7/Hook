using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class FishingLocationConfiguration : IEntityTypeConfiguration<FishingLocation>
{
    public void Configure(EntityTypeBuilder<FishingLocation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Governorate)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "القاهرة", Governorate = "Cairo", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "الإسكندرية", Governorate = "Alexandria", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "الجيزة", Governorate = "Giza", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "بورسعيد", Governorate = "Port Said", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "السويس", Governorate = "Suez", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "دمياط", Governorate = "Damietta", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "الدقهلية", Governorate = "Dakahlia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "الشرقية", Governorate = "Sharkia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "الغربية", Governorate = "Gharbia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "كفر الشيخ", Governorate = "Kafr El Sheikh", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "المنوفية", Governorate = "Monufia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "البحيرة", Governorate = "Beheira", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "القليوبية", Governorate = "Qalyubia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "الإسماعيلية", Governorate = "Ismailia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000015"), Name = "الفيوم", Governorate = "Faiyum", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000016"), Name = "بني سويف", Governorate = "Beni Suef", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000017"), Name = "المنيا", Governorate = "Minya", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000018"), Name = "أسيوط", Governorate = "Assiut", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000019"), Name = "سوهاج", Governorate = "Sohag", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000020"), Name = "قنا", Governorate = "Qena", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000021"), Name = "الأقصر", Governorate = "Luxor", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000022"), Name = "أسوان", Governorate = "Aswan", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000023"), Name = "البحر الأحمر", Governorate = "Red Sea", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000024"), Name = "الوادي الجديد", Governorate = "New Valley", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000025"), Name = "مطروح", Governorate = "Matrouh", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000026"), Name = "شمال سيناء", Governorate = "North Sinai", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new FishingLocation { Id = Guid.Parse("00000000-0000-0000-0000-000000000027"), Name = "جنوب سيناء", Governorate = "South Sinai", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
