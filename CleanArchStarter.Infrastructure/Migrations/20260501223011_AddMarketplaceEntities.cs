using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketplaceEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketplaceListingRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdminRejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceListingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceListingRequest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SellerName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    NationalIdPhotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdminRejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceListingRequestImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListingRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsMainImage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceListingRequestImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceListingRequestImage_MarketplaceListingRequest_ListingRequestId",
                        column: x => x.ListingRequestId,
                        principalTable: "MarketplaceListingRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceOrder_AspNetUsers_BuyerUserId",
                        column: x => x.BuyerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketplaceOrder_SellerProfile_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceProduct_SellerProfile_SellerProfileId",
                        column: x => x.SellerProfileId,
                        principalTable: "SellerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceCartItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceCartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceCartItem_AspNetUsers_BuyerUserId",
                        column: x => x.BuyerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketplaceCartItem_MarketplaceProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MarketplaceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceOrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceOrderItem_MarketplaceOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "MarketplaceOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketplaceOrderItem_MarketplaceProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MarketplaceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsMainImage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceProductImage_MarketplaceProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MarketplaceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceReview",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketplaceReview_AspNetUsers_BuyerUserId",
                        column: x => x.BuyerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketplaceReview_MarketplaceOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "MarketplaceOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketplaceReview_MarketplaceProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MarketplaceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.Apply", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.ViewProfile", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.ViewAll", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Seller.Restore", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Products.Delete", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.UpdateStatus", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.Cancel", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Orders.Stats", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Cart.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Cart.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Reviews.Create", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Reviews.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Admin.ViewSellers", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Admin.DeleteSeller", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Admin.ViewProducts", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Admin.DeleteProduct", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Approvals.View", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Marketplace.Approvals.Update", "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 75, "Permissions", "Permissions.Bookings.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 76, "Permissions", "Permissions.Payments.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 77, "Permissions", "Permissions.Payments.UploadReceipt", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 78, "Permissions", "Permissions.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 79, "Permissions", "Permissions.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 80, "Permissions", "Permissions.Reviews.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 81, "Permissions", "Permissions.Reviews.Delete", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 82, "Permissions", "Permissions.Seller.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 83, "Permissions", "Permissions.Seller.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 84, "Permissions", "Permissions.Marketplace.Products.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 85, "Permissions", "Permissions.Marketplace.Cart.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 86, "Permissions", "Permissions.Marketplace.Cart.Update", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 87, "Permissions", "Permissions.Marketplace.Orders.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 88, "Permissions", "Permissions.Marketplace.Orders.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 89, "Permissions", "Permissions.Marketplace.Orders.Cancel", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 90, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 91, "Permissions", "Permissions.Marketplace.Reviews.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 92, "Permissions", "Permissions.Marketplace.Reviews.Create", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 93, "Permissions", "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 94, "Permissions", "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 95, "Permissions", "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 96, "Permissions", "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 97, "Permissions", "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 98, "Permissions", "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 99, "Permissions", "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 100, "Permissions", "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 101, "Permissions", "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 102, "Permissions", "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 103, "Permissions", "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 104, "Permissions", "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 105, "Permissions", "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 106, "Permissions", "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 107, "Permissions", "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 108, "Permissions", "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 109, "Permissions", "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" },
                    { 110, "Permissions", "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[] { "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b", "c2b1d0e9-6e7d-4d8f-9a3b-1f2e3d4c5b6a", false, false, "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 111, "Permissions", "Permissions.Users.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 112, "Permissions", "Permissions.Users.UpdateProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 113, "Permissions", "Permissions.Users.ChangePassword", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 114, "Permissions", "Permissions.Seller.ViewProfile", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 115, "Permissions", "Permissions.Marketplace.Products.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 116, "Permissions", "Permissions.Marketplace.Products.Create", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 117, "Permissions", "Permissions.Marketplace.Products.Update", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 118, "Permissions", "Permissions.Marketplace.Products.Delete", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 119, "Permissions", "Permissions.Marketplace.Orders.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 120, "Permissions", "Permissions.Marketplace.Orders.UpdateStatus", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 121, "Permissions", "Permissions.Marketplace.Orders.Stats", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" },
                    { 122, "Permissions", "Permissions.Marketplace.Reviews.View", "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceCartItem_BuyerUserId_ProductId",
                table: "MarketplaceCartItem",
                columns: new[] { "BuyerUserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceCartItem_ProductId",
                table: "MarketplaceCartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceListingRequest_UserId",
                table: "MarketplaceListingRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceListingRequestImage_ListingRequestId",
                table: "MarketplaceListingRequestImage",
                column: "ListingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceOrder_BuyerUserId",
                table: "MarketplaceOrder",
                column: "BuyerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceOrder_SellerProfileId",
                table: "MarketplaceOrder",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceOrderItem_OrderId",
                table: "MarketplaceOrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceOrderItem_ProductId",
                table: "MarketplaceOrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceProduct_SellerProfileId",
                table: "MarketplaceProduct",
                column: "SellerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceProductImage_ProductId",
                table: "MarketplaceProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceReview_BuyerUserId_ProductId_OrderId",
                table: "MarketplaceReview",
                columns: new[] { "BuyerUserId", "ProductId", "OrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceReview_OrderId",
                table: "MarketplaceReview",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceReview_ProductId",
                table: "MarketplaceReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfile_UserId",
                table: "SellerProfile",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketplaceCartItem");

            migrationBuilder.DropTable(
                name: "MarketplaceListingRequestImage");

            migrationBuilder.DropTable(
                name: "MarketplaceOrderItem");

            migrationBuilder.DropTable(
                name: "MarketplaceProductImage");

            migrationBuilder.DropTable(
                name: "MarketplaceReview");

            migrationBuilder.DropTable(
                name: "MarketplaceListingRequest");

            migrationBuilder.DropTable(
                name: "MarketplaceOrder");

            migrationBuilder.DropTable(
                name: "MarketplaceProduct");

            migrationBuilder.DropTable(
                name: "SellerProfile");

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.Apply", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" });

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
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.UpdateProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Users.ChangePassword", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.BoatOwner.ViewProfile", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Boats.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Create", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Update", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Trips.Delete", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Bookings.UpdateStatus", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.Verify", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Payments.Stats", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Permissions.Reviews.View", "42bf2b74-278d-453f-acd7-52d09bbcdcb3" });
        }
    }
}
