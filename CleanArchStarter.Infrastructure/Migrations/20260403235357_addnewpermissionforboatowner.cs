using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewpermissionforboatowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 14, "Permissions", "Permissions.BoatOwner.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 15, "Permissions", "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 16, "Permissions", "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 17, "Permissions", "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 18, "Permissions", "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 19, "Permissions", "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 20, "Permissions", "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 21, "Permissions", "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 22, "Permissions", "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 23, "Permissions", "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });
        }
    }
}
