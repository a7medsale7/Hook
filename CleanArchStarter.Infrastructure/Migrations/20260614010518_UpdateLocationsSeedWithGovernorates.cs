using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocationsSeedWithGovernorates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.InsertData(
                table: "FishingLocations",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Governorate", "IsDeleted", "Latitude", "Longitude", "Name", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cairo", false, 0.0, 0.0, "القاهرة", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Alexandria", false, 0.0, 0.0, "الإسكندرية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Giza", false, 0.0, 0.0, "الجيزة", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Port Said", false, 0.0, 0.0, "بورسعيد", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Suez", false, 0.0, 0.0, "السويس", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Damietta", false, 0.0, 0.0, "دمياط", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dakahlia", false, 0.0, 0.0, "الدقهلية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sharkia", false, 0.0, 0.0, "الشرقية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gharbia", false, 0.0, 0.0, "الغربية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kafr El Sheikh", false, 0.0, 0.0, "كفر الشيخ", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Monufia", false, 0.0, 0.0, "المنوفية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Beheira", false, 0.0, 0.0, "البحيرة", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Qalyubia", false, 0.0, 0.0, "القليوبية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ismailia", false, 0.0, 0.0, "الإسماعيلية", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Faiyum", false, 0.0, 0.0, "الفيوم", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Beni Suef", false, 0.0, 0.0, "بني سويف", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Minya", false, 0.0, 0.0, "المنيا", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Assiut", false, 0.0, 0.0, "أسيوط", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sohag", false, 0.0, 0.0, "سوهاج", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Qena", false, 0.0, 0.0, "قنا", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Luxor", false, 0.0, 0.0, "الأقصر", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Aswan", false, 0.0, 0.0, "أسوان", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Red Sea", false, 0.0, 0.0, "البحر الأحمر", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "New Valley", false, 0.0, 0.0, "الوادي الجديد", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Matrouh", false, 0.0, 0.0, "مطروح", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "North Sinai", false, 0.0, 0.0, "شمال سيناء", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "South Sinai", false, 0.0, 0.0, "جنوب سيناء", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "FishingLocations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"));

            migrationBuilder.InsertData(
                table: "FishingLocations",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Governorate", "IsDeleted", "Latitude", "Longitude", "Name", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "الإسكندرية (Alexandria)", false, 31.111799999999999, 29.761800000000001, "شاطئ النخيل (El Nakheel)", null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "الغردقة (Hurghada)", false, 27.218499999999999, 33.843600000000002, "شاطئ الشيراتون (Sheraton)", null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "السويس (Suez)", false, 29.9572, 32.553199999999997, "ميناء السويس (Suez Port)", null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "دمياط (Damietta)", false, 31.523199999999999, 31.815200000000001, "رأس البر (Ras El Bar)", null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "شرم الشيخ (Sharm El Sheikh)", false, 27.863099999999999, 34.296500000000002, "شرم الميه (Sharm El Maya)", null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "أسوان (Aswan)", false, 23.978200000000001, 32.868200000000002, "بحيرة ناصر (Lake Nasser)", null, null }
                });
        }
    }
}
