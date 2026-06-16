using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFishGuardAdminPermissionToCommunityAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.FishGuard.Admin.Manage", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 84,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 85,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 86,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 87,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 88,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 89,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 90,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 91,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 92,
                column: "ClaimValue",
                value: "Permissions.Bookings.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 93,
                column: "ClaimValue",
                value: "Permissions.Bookings.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 94,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 95,
                column: "ClaimValue",
                value: "Permissions.Payments.UploadReceipt");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 96,
                column: "ClaimValue",
                value: "Permissions.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 97,
                column: "ClaimValue",
                value: "Permissions.Reviews.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 98,
                column: "ClaimValue",
                value: "Permissions.Reviews.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 99,
                column: "ClaimValue",
                value: "Permissions.Reviews.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 100,
                column: "ClaimValue",
                value: "Permissions.Seller.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101,
                column: "ClaimValue",
                value: "Permissions.Seller.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 103,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Cart.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Cart.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 105,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 106,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 110,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Like");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Save");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Report");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                column: "ClaimValue",
                value: "Permissions.Community.User.Follow");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Join");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Participants.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                column: "ClaimValue",
                value: "Permissions.Community.Feed.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Complaints.Support", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 126,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 127,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 128,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 129,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 130,
                column: "ClaimValue",
                value: "Permissions.Boats.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131,
                column: "ClaimValue",
                value: "Permissions.Boats.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 132,
                column: "ClaimValue",
                value: "Permissions.Boats.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 133,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 134,
                column: "ClaimValue",
                value: "Permissions.Trips.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 135,
                column: "ClaimValue",
                value: "Permissions.Trips.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 136,
                column: "ClaimValue",
                value: "Permissions.Trips.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 137,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138,
                column: "ClaimValue",
                value: "Permissions.Bookings.ViewAll");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139,
                column: "ClaimValue",
                value: "Permissions.Bookings.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141,
                column: "ClaimValue",
                value: "Permissions.Payments.Verify");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 142,
                column: "ClaimValue",
                value: "Permissions.Payments.Stats");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 143,
                column: "ClaimValue",
                value: "Permissions.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 144,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 145,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 146,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 147,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Like");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 148,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Save");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 149,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Report");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 150,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 152,
                column: "ClaimValue",
                value: "Permissions.Community.User.Follow");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 153,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Join");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 154,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Participants.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155,
                column: "ClaimValue",
                value: "Permissions.Community.Feed.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 156,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Complaints.Support", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 158,
                column: "ClaimValue",
                value: "Permissions.Users.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 160,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 161,
                column: "ClaimValue",
                value: "Permissions.Seller.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 168,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Stats");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 170,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 171,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 172,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 173,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Like");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 174,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Save");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 175,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Report");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 176,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 177,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 178,
                column: "ClaimValue",
                value: "Permissions.Community.User.Follow");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 179,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Join");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 180,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Participants.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 181,
                column: "ClaimValue",
                value: "Permissions.Community.Feed.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 182,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Complaints.Support", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 184,
                column: "ClaimValue",
                value: "Permissions.Community.Admin.Complaints.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 185,
                column: "ClaimValue",
                value: "Permissions.Community.Admin.Complaints.Resolve");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 186, "Permissions", "Permissions.Community.Notifications.View", "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a" },
                    { 187, "Permissions", "Permissions.FishGuard.Admin.Manage", "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 84,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 85,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 86,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 87,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 88,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 89,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 90,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 91,
                column: "ClaimValue",
                value: "Permissions.Bookings.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 92,
                column: "ClaimValue",
                value: "Permissions.Bookings.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 93,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 94,
                column: "ClaimValue",
                value: "Permissions.Payments.UploadReceipt");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 95,
                column: "ClaimValue",
                value: "Permissions.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 96,
                column: "ClaimValue",
                value: "Permissions.Reviews.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 97,
                column: "ClaimValue",
                value: "Permissions.Reviews.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 98,
                column: "ClaimValue",
                value: "Permissions.Reviews.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 99,
                column: "ClaimValue",
                value: "Permissions.Seller.Apply");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 100,
                column: "ClaimValue",
                value: "Permissions.Seller.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Cart.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 103,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Cart.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 105,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 106,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 110,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Like");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Save");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Report");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                column: "ClaimValue",
                value: "Permissions.Community.User.Follow");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Join");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Participants.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                column: "ClaimValue",
                value: "Permissions.Community.Feed.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                column: "ClaimValue",
                value: "Permissions.Community.Complaints.Support");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 126,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 127,
                column: "ClaimValue",
                value: "Permissions.BoatOwner.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 128,
                column: "ClaimValue",
                value: "Permissions.Boats.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 129,
                column: "ClaimValue",
                value: "Permissions.Boats.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 130,
                column: "ClaimValue",
                value: "Permissions.Boats.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131,
                column: "ClaimValue",
                value: "Permissions.Boats.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 132,
                column: "ClaimValue",
                value: "Permissions.Trips.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 133,
                column: "ClaimValue",
                value: "Permissions.Trips.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 134,
                column: "ClaimValue",
                value: "Permissions.Trips.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 135,
                column: "ClaimValue",
                value: "Permissions.Trips.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 136,
                column: "ClaimValue",
                value: "Permissions.Bookings.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 137,
                column: "ClaimValue",
                value: "Permissions.Bookings.ViewAll");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138,
                column: "ClaimValue",
                value: "Permissions.Bookings.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139,
                column: "ClaimValue",
                value: "Permissions.Payments.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140,
                column: "ClaimValue",
                value: "Permissions.Payments.Verify");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141,
                column: "ClaimValue",
                value: "Permissions.Payments.Stats");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 142,
                column: "ClaimValue",
                value: "Permissions.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 143,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 144,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 145,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 146,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Like");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 147,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Save");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 148,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Report");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 149,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 150,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151,
                column: "ClaimValue",
                value: "Permissions.Community.User.Follow");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 152,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Join");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 153,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Participants.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 154,
                column: "ClaimValue",
                value: "Permissions.Community.Feed.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 156,
                column: "ClaimValue",
                value: "Permissions.Community.Complaints.Support");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 158,
                column: "ClaimValue",
                value: "Permissions.Users.UpdateProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159,
                column: "ClaimValue",
                value: "Permissions.Users.ChangePassword");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 160,
                column: "ClaimValue",
                value: "Permissions.Seller.ViewProfile");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 161,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Products.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.UpdateStatus");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Orders.Stats");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 168,
                column: "ClaimValue",
                value: "Permissions.Marketplace.Reviews.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 170,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 171,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 172,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Like");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 173,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Save");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 174,
                column: "ClaimValue",
                value: "Permissions.Community.Posts.Report");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 175,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 176,
                column: "ClaimValue",
                value: "Permissions.Community.Comments.Delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 177,
                column: "ClaimValue",
                value: "Permissions.Community.User.Follow");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 178,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Join");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 179,
                column: "ClaimValue",
                value: "Permissions.Community.Events.Participants.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 180,
                column: "ClaimValue",
                value: "Permissions.Community.Feed.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 181,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 182,
                column: "ClaimValue",
                value: "Permissions.Community.Complaints.Support");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Community.Admin.Complaints.View", "8a2f4c3a-1b2c-3d4e-5f6a-7b8c9d0e1f2a" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 184,
                column: "ClaimValue",
                value: "Permissions.Community.Admin.Complaints.Resolve");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 185,
                column: "ClaimValue",
                value: "Permissions.Community.Notifications.View");
        }
    }
}
