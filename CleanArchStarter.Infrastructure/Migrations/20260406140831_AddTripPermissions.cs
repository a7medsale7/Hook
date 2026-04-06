using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTripPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "RoleId",
                value: "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "RoleId",
                value: "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "RoleId",
                value: "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 35, "Permissions", "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 36, "Permissions", "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 37, "Permissions", "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 38, "Permissions", "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 39, "Permissions", "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 40, "Permissions", "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 41, "Permissions", "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 42, "Permissions", "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 43, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 44, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 45, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "RoleId",
                value: "42bf2b74-278d-453f-acd7-52d09bbcdcb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "RoleId",
                value: "42bf2b74-278d-453f-acd7-52d09bbcdcb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "RoleId",
                value: "42bf2b74-278d-453f-acd7-52d09bbcdcb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34,
                column: "ClaimValue",
                value: "Permissions.Boats.Delete");
        }
    }
}
