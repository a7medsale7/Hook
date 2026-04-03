using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminHash_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "877a5585-4894-4f4b-8989-f45476063ce1",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIoxWb26cTzdfCumxCiMdXzQAS4fntJbgyxCwE8JfNYexufyjyYGQL4kmp0ydKXiJA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "877a5585-4894-4f4b-8989-f45476063ce1",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBu6H7C9iF9Tq3twVRr0uJaFzrtfpWgkhEKBHa0jsAD+KuMRutaglQhMbnbICcDKyQ==");
        }
    }
}
