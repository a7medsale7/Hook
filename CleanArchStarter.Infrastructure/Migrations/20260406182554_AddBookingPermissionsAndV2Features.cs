using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingPermissionsAndV2Features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Cancel", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 46, "Permissions", "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 47, "Permissions", "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 48, "Permissions", "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 49, "Permissions", "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 50, "Permissions", "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 51, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 52, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 53, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 54, "Permissions", "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 55, "Permissions", "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

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
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                column: "ClaimValue",
                value: "Permissions.Trips.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                column: "ClaimValue",
                value: "Permissions.Trips.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "ClaimValue",
                value: "Permissions.Trips.Delete");
        }
    }
}
