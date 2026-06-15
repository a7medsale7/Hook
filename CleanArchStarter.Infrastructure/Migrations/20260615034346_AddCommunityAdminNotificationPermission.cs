using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityAdminNotificationPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 185, "Permissions", "Permissions.Community.Notifications.View", "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 185);
        }
    }
}
