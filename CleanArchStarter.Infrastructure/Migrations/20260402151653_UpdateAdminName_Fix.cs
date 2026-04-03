using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminName_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "877a5585-4894-4f4b-8989-f45476063ce1",
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Admin", "Account" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "877a5585-4894-4f4b-8989-f45476063ce1",
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Malaeb", "Admin" });
        }
    }
}
