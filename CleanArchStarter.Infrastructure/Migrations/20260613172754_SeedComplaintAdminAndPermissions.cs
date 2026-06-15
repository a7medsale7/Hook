using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedComplaintAdminAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActorUserId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Like", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Save", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Comments.Add", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Comments.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.User.Follow", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Events.Join", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Events.Participants.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Feed.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Notifications.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Admin.Complaints.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Admin.Complaints.Resolve", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 81,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 82,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 83,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 84,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 85,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 86,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 87,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 88,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 89,
                column: "ClaimValue",
                value: "Permissions.Bookings.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 90,
                column: "ClaimValue",
                value: "Permissions.Bookings.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 91,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 92,
                column: "ClaimValue",
                value: "Permissions.Payments.UploadReceipt");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 93,
                column: "ClaimValue",
                value: "Permissions.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Cart.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Cart.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.UpdateStatus", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Like", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Posts.Save", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Comments.Add", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Comments.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.User.Follow", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Events.Join", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Events.Participants.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Feed.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Notifications.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 125, "Permissions", "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 126, "Permissions", "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 127, "Permissions", "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 128, "Permissions", "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 129, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 130, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 131, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 132, "Permissions", "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 133, "Permissions", "Permissions.Bookings.ViewAll", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 134, "Permissions", "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 135, "Permissions", "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 136, "Permissions", "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 137, "Permissions", "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 138, "Permissions", "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 139, "Permissions", "Permissions.Community.Posts.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 140, "Permissions", "Permissions.Community.Posts.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 141, "Permissions", "Permissions.Community.Posts.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 142, "Permissions", "Permissions.Community.Posts.Like", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 143, "Permissions", "Permissions.Community.Posts.Save", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 144, "Permissions", "Permissions.Community.Comments.Add", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 145, "Permissions", "Permissions.Community.Comments.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 146, "Permissions", "Permissions.Community.User.Follow", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 147, "Permissions", "Permissions.Community.Events.Join", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 148, "Permissions", "Permissions.Community.Events.Participants.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 149, "Permissions", "Permissions.Community.Feed.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 150, "Permissions", "Permissions.Community.Notifications.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 151, "Permissions", "Permissions.Users.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 152, "Permissions", "Permissions.Users.UpdateProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 153, "Permissions", "Permissions.Users.ChangePassword", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 154, "Permissions", "Permissions.Seller.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 155, "Permissions", "Permissions.Marketplace.Products.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 156, "Permissions", "Permissions.Marketplace.Products.Create", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 157, "Permissions", "Permissions.Marketplace.Products.Update", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 158, "Permissions", "Permissions.Marketplace.Products.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 159, "Permissions", "Permissions.Marketplace.Orders.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 160, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 161, "Permissions", "Permissions.Marketplace.Orders.Stats", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 162, "Permissions", "Permissions.Marketplace.Reviews.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 163, "Permissions", "Permissions.Community.Posts.Create", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 164, "Permissions", "Permissions.Community.Posts.Update", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 165, "Permissions", "Permissions.Community.Posts.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 166, "Permissions", "Permissions.Community.Posts.Like", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 167, "Permissions", "Permissions.Community.Posts.Save", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 168, "Permissions", "Permissions.Community.Comments.Add", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 169, "Permissions", "Permissions.Community.Comments.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 170, "Permissions", "Permissions.Community.User.Follow", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 171, "Permissions", "Permissions.Community.Events.Join", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 172, "Permissions", "Permissions.Community.Events.Participants.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 173, "Permissions", "Permissions.Community.Feed.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 174, "Permissions", "Permissions.Community.Notifications.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Governorate", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c", 0, null, "b5a4c3d2-e1f0-4a9b-8c7d-6e5f4d3c2b1a", "complaintadmin@hook.com", true, "Complaint", null, false, "Admin", false, null, "COMPLAINTADMIN@HOOK.COM", "COMPLAINTADMIN@HOOK.COM", "AQAAAAIAAYagAAAAEDZ2h6sin7MY4jaCkLZ14ouGEAkMnGEl+CJAnncaPMHLcJwuv7Dk0w8470nceXMYQg==", null, false, null, "A7B8C9D0E1F234567890ABCDEF123456", false, "complaintadmin@hook.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ActorUserId",
                table: "Notifications",
                column: "ActorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ActorUserId",
                table: "Notifications",
                column: "ActorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ActorUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ActorUserId",
                table: "Notifications");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7a8b9c0-1d2e-3f4a-5b6c-7d8e9f0a1b2c");

            migrationBuilder.DropColumn(
                name: "ActorUserId",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.UploadReceipt", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 81,
                column: "ClaimValue",
                value: "Permissions.Reviews.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 82,
                column: "ClaimValue",
                value: "Permissions.Reviews.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 83,
                column: "ClaimValue",
                value: "Permissions.Seller.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 84,
                column: "ClaimValue",
                value: "Permissions.Seller.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 85,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 86,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Cart.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 87,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Cart.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 88,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 89,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 90,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 91,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 92,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 93,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.ViewAll", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.Create", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.Update", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.UpdateStatus", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.Stats", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Reviews.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });
        }
    }
}
