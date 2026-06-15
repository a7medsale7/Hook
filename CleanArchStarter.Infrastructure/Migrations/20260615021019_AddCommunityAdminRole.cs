using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[] { "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a", "b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e", false, false, "CommunityAdmin", "COMMUNITYADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 183, "Permissions", "Permissions.Community.Admin.Complaints.View", "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a" },
                    { 184, "Permissions", "Permissions.Community.Admin.Complaints.Resolve", "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a", "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a", "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c" });
        }
    }
}
