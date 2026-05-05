using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalSyncFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoleClaims");
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2000);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3000);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3001);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3002);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3003);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3004);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3005);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3006);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3007);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3008);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3009);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3010);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3011);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5000);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5001);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5002);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5003);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5004);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5005);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5006);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5007);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5008);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5009);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5010);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5011);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5012);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5013);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5014);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5015);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5016);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5017);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5018);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5019);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5020);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5021);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5022);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5023);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5024);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5025);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5026);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5027);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5028);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5029);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5030);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5031);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5032);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5033);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5034);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5035);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5036);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5037);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5038);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5039);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5040);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5041);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5042);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5043);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5044);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5045);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5046);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5047);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5048);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5049);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5050);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5051);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5052);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5053);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5054);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5055);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5056);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5057);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5058);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5059);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5060);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5061);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5062);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5063);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5064);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5065);

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permissions", "Permissions", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 2, "Permissions", "Permissions.Users.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 3, "Permissions", "Permissions.Users.UpdateProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 4, "Permissions", "Permissions.Users.ChangePassword", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5, "Permissions", "Permissions.Users.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 6, "Permissions", "Permissions.Users.ManageRoles", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 7, "Permissions", "Permissions.Roles.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 8, "Permissions", "Permissions.Roles.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 9, "Permissions", "Permissions.Roles.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 10, "Permissions", "Permissions.Roles.ToggleActive", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 11, "Permissions", "Permissions.BoatOwner.Apply", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 12, "Permissions", "Permissions.BoatOwner.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 13, "Permissions", "Permissions.BoatOwner.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 14, "Permissions", "Permissions.BoatOwner.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 15, "Permissions", "Permissions.BoatOwner.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 16, "Permissions", "Permissions.BoatOwner.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 17, "Permissions", "Permissions.Boats.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 18, "Permissions", "Permissions.Boats.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 19, "Permissions", "Permissions.Boats.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 20, "Permissions", "Permissions.Boats.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 21, "Permissions", "Permissions.Boats.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 22, "Permissions", "Permissions.Trips.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 23, "Permissions", "Permissions.Trips.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 24, "Permissions", "Permissions.Trips.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 25, "Permissions", "Permissions.Trips.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 26, "Permissions", "Permissions.Trips.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 27, "Permissions", "Permissions.Bookings.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 28, "Permissions", "Permissions.Bookings.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 29, "Permissions", "Permissions.Bookings.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 30, "Permissions", "Permissions.Bookings.Cancel", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 31, "Permissions", "Permissions.Bookings.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 32, "Permissions", "Permissions.Bookings.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 33, "Permissions", "Permissions.Payments.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 34, "Permissions", "Permissions.Payments.UploadReceipt", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 35, "Permissions", "Permissions.Payments.Verify", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 36, "Permissions", "Permissions.Payments.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 37, "Permissions", "Permissions.Payments.Stats", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 38, "Permissions", "Permissions.Reviews.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 39, "Permissions", "Permissions.Reviews.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 40, "Permissions", "Permissions.Reviews.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 41, "Permissions", "Permissions.Reviews.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 42, "Permissions", "Permissions.Seller.Apply", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 43, "Permissions", "Permissions.Seller.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 44, "Permissions", "Permissions.Seller.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 45, "Permissions", "Permissions.Seller.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 46, "Permissions", "Permissions.Seller.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 47, "Permissions", "Permissions.Seller.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 48, "Permissions", "Permissions.Marketplace.Products.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 49, "Permissions", "Permissions.Marketplace.Products.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 50, "Permissions", "Permissions.Marketplace.Products.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 51, "Permissions", "Permissions.Marketplace.Products.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 52, "Permissions", "Permissions.Marketplace.Orders.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 53, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 54, "Permissions", "Permissions.Marketplace.Orders.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 55, "Permissions", "Permissions.Marketplace.Orders.Cancel", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 56, "Permissions", "Permissions.Marketplace.Orders.Stats", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 57, "Permissions", "Permissions.Marketplace.Cart.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 58, "Permissions", "Permissions.Marketplace.Cart.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 59, "Permissions", "Permissions.Marketplace.Reviews.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 60, "Permissions", "Permissions.Marketplace.Reviews.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 61, "Permissions", "Permissions.Marketplace.Admin.ViewSellers", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 62, "Permissions", "Permissions.Marketplace.Admin.DeleteSeller", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 63, "Permissions", "Permissions.Marketplace.Admin.ViewProducts", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 64, "Permissions", "Permissions.Marketplace.Admin.DeleteProduct", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 65, "Permissions", "Permissions.Marketplace.Approvals.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 66, "Permissions", "Permissions.Marketplace.Approvals.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 67, "Permissions", "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 68, "Permissions", "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 69, "Permissions", "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 70, "Permissions", "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 71, "Permissions", "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 72, "Permissions", "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 73, "Permissions", "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 74, "Permissions", "Permissions.Bookings.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 75, "Permissions", "Permissions.Bookings.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 76, "Permissions", "Permissions.Bookings.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 77, "Permissions", "Permissions.Payments.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 78, "Permissions", "Permissions.Payments.UploadReceipt", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 79, "Permissions", "Permissions.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 80, "Permissions", "Permissions.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 81, "Permissions", "Permissions.Reviews.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 82, "Permissions", "Permissions.Reviews.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 83, "Permissions", "Permissions.Seller.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 84, "Permissions", "Permissions.Seller.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 85, "Permissions", "Permissions.Marketplace.Products.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 86, "Permissions", "Permissions.Marketplace.Cart.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 87, "Permissions", "Permissions.Marketplace.Cart.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 88, "Permissions", "Permissions.Marketplace.Orders.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 89, "Permissions", "Permissions.Marketplace.Orders.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 90, "Permissions", "Permissions.Marketplace.Orders.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 91, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 92, "Permissions", "Permissions.Marketplace.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 93, "Permissions", "Permissions.Marketplace.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 94, "Permissions", "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 95, "Permissions", "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 96, "Permissions", "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 97, "Permissions", "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 98, "Permissions", "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 99, "Permissions", "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 100, "Permissions", "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 101, "Permissions", "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 102, "Permissions", "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 103, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 104, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 105, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 106, "Permissions", "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 107, "Permissions", "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 108, "Permissions", "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 109, "Permissions", "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 110, "Permissions", "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 111, "Permissions", "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 112, "Permissions", "Permissions.Users.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 113, "Permissions", "Permissions.Users.UpdateProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 114, "Permissions", "Permissions.Users.ChangePassword", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 115, "Permissions", "Permissions.Seller.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 116, "Permissions", "Permissions.Marketplace.Products.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 117, "Permissions", "Permissions.Marketplace.Products.Create", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 118, "Permissions", "Permissions.Marketplace.Products.Update", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 119, "Permissions", "Permissions.Marketplace.Products.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 120, "Permissions", "Permissions.Marketplace.Orders.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 121, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 122, "Permissions", "Permissions.Marketplace.Orders.Stats", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 123, "Permissions", "Permissions.Marketplace.Reviews.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

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

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

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

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65);

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

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1000, "Permissions", "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1001, "Permissions", "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1002, "Permissions", "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1003, "Permissions", "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1004, "Permissions", "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1005, "Permissions", "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1006, "Permissions", "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1007, "Permissions", "Permissions.Bookings.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1008, "Permissions", "Permissions.Bookings.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1009, "Permissions", "Permissions.Bookings.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1010, "Permissions", "Permissions.Payments.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1011, "Permissions", "Permissions.Payments.UploadReceipt", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1012, "Permissions", "Permissions.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1013, "Permissions", "Permissions.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1014, "Permissions", "Permissions.Reviews.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1015, "Permissions", "Permissions.Reviews.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1016, "Permissions", "Permissions.Seller.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1017, "Permissions", "Permissions.Seller.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1018, "Permissions", "Permissions.Marketplace.Products.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1019, "Permissions", "Permissions.Marketplace.Cart.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1020, "Permissions", "Permissions.Marketplace.Cart.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1021, "Permissions", "Permissions.Marketplace.Orders.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1022, "Permissions", "Permissions.Marketplace.Orders.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1023, "Permissions", "Permissions.Marketplace.Orders.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1024, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1025, "Permissions", "Permissions.Marketplace.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 1026, "Permissions", "Permissions.Marketplace.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 2000, "Permissions", "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2001, "Permissions", "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2002, "Permissions", "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2003, "Permissions", "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2004, "Permissions", "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2005, "Permissions", "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2006, "Permissions", "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2007, "Permissions", "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2008, "Permissions", "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2009, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2010, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2011, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2012, "Permissions", "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2013, "Permissions", "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2014, "Permissions", "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2015, "Permissions", "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2016, "Permissions", "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 2017, "Permissions", "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 3000, "Permissions", "Permissions.Users.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3001, "Permissions", "Permissions.Users.UpdateProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3002, "Permissions", "Permissions.Users.ChangePassword", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3003, "Permissions", "Permissions.Seller.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3004, "Permissions", "Permissions.Marketplace.Products.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3005, "Permissions", "Permissions.Marketplace.Products.Create", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3006, "Permissions", "Permissions.Marketplace.Products.Update", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3007, "Permissions", "Permissions.Marketplace.Products.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3008, "Permissions", "Permissions.Marketplace.Orders.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3009, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3010, "Permissions", "Permissions.Marketplace.Orders.Stats", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 3011, "Permissions", "Permissions.Marketplace.Reviews.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 5000, "Permissions", "Permissions", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5001, "Permissions", "Permissions.Users.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5002, "Permissions", "Permissions.Users.UpdateProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5003, "Permissions", "Permissions.Users.ChangePassword", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5004, "Permissions", "Permissions.Users.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5005, "Permissions", "Permissions.Users.ManageRoles", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5006, "Permissions", "Permissions.Roles.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5007, "Permissions", "Permissions.Roles.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5008, "Permissions", "Permissions.Roles.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5009, "Permissions", "Permissions.Roles.ToggleActive", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5010, "Permissions", "Permissions.BoatOwner.Apply", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5011, "Permissions", "Permissions.BoatOwner.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5012, "Permissions", "Permissions.BoatOwner.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5013, "Permissions", "Permissions.BoatOwner.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5014, "Permissions", "Permissions.BoatOwner.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5015, "Permissions", "Permissions.BoatOwner.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5016, "Permissions", "Permissions.Boats.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5017, "Permissions", "Permissions.Boats.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5018, "Permissions", "Permissions.Boats.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5019, "Permissions", "Permissions.Boats.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5020, "Permissions", "Permissions.Boats.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5021, "Permissions", "Permissions.Trips.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5022, "Permissions", "Permissions.Trips.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5023, "Permissions", "Permissions.Trips.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5024, "Permissions", "Permissions.Trips.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5025, "Permissions", "Permissions.Trips.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5026, "Permissions", "Permissions.Bookings.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5027, "Permissions", "Permissions.Bookings.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5028, "Permissions", "Permissions.Bookings.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5029, "Permissions", "Permissions.Bookings.Cancel", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5030, "Permissions", "Permissions.Bookings.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5031, "Permissions", "Permissions.Bookings.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5032, "Permissions", "Permissions.Payments.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5033, "Permissions", "Permissions.Payments.UploadReceipt", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5034, "Permissions", "Permissions.Payments.Verify", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5035, "Permissions", "Permissions.Payments.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5036, "Permissions", "Permissions.Payments.Stats", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5037, "Permissions", "Permissions.Reviews.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5038, "Permissions", "Permissions.Reviews.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5039, "Permissions", "Permissions.Reviews.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5040, "Permissions", "Permissions.Reviews.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5041, "Permissions", "Permissions.Seller.Apply", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5042, "Permissions", "Permissions.Seller.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5043, "Permissions", "Permissions.Seller.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5044, "Permissions", "Permissions.Seller.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5045, "Permissions", "Permissions.Seller.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5046, "Permissions", "Permissions.Seller.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5047, "Permissions", "Permissions.Marketplace.Products.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5048, "Permissions", "Permissions.Marketplace.Products.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5049, "Permissions", "Permissions.Marketplace.Products.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5050, "Permissions", "Permissions.Marketplace.Products.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5051, "Permissions", "Permissions.Marketplace.Orders.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5052, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5053, "Permissions", "Permissions.Marketplace.Orders.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5054, "Permissions", "Permissions.Marketplace.Orders.Cancel", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5055, "Permissions", "Permissions.Marketplace.Orders.Stats", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5056, "Permissions", "Permissions.Marketplace.Cart.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5057, "Permissions", "Permissions.Marketplace.Cart.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5058, "Permissions", "Permissions.Marketplace.Reviews.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5059, "Permissions", "Permissions.Marketplace.Reviews.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5060, "Permissions", "Permissions.Marketplace.Admin.ViewSellers", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5061, "Permissions", "Permissions.Marketplace.Admin.DeleteSeller", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5062, "Permissions", "Permissions.Marketplace.Admin.ViewProducts", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5063, "Permissions", "Permissions.Marketplace.Admin.DeleteProduct", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5064, "Permissions", "Permissions.Marketplace.Approvals.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" },
                    { 5065, "Permissions", "Permissions.Marketplace.Approvals.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" }
                });
        }
    }
}
