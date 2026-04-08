using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewSystemFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.UploadReceipt", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62,
                column: "ClaimValue",
                value: "Permissions.Boats.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63,
                column: "ClaimValue",
                value: "Permissions.Boats.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64,
                column: "ClaimValue",
                value: "Permissions.Boats.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 66, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 67, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 68, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 69, "Permissions", "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 70, "Permissions", "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 71, "Permissions", "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 72, "Permissions", "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 73, "Permissions", "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 74, "Permissions", "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "ClaimValue",
                value: "Permissions.Bookings.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                column: "ClaimValue",
                value: "Permissions.Bookings.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                column: "ClaimValue",
                value: "Permissions.Payments.UploadReceipt");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58,
                column: "ClaimValue",
                value: "Permissions.Trips.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59,
                column: "ClaimValue",
                value: "Permissions.Trips.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60,
                column: "ClaimValue",
                value: "Permissions.Trips.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62,
                column: "ClaimValue",
                value: "Permissions.Bookings.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64,
                column: "ClaimValue",
                value: "Permissions.Payments.Verify");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65,
                column: "ClaimValue",
                value: "Permissions.Payments.Stats");
        }
    }
}
