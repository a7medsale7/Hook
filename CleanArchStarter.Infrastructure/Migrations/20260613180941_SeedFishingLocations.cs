using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedFishingLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
