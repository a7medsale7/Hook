using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "d1c8e5b9-9f0a-4c3e-8a1b-2f5e6d7c8a9b", false, false, "Admin", "ADMIN" },
                    { "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9", "f3a7b8c9-0d2e-4f5a-9b3c-4d6e7f8a9b1c", true, false, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "877a5585-4894-4f4b-8989-f45476063ce1", 0, "a25eba83-ab00-408b-8dfa-ce8a1cf37bea", "AdminAccount@gmail.com", true, "Malaeb", false, "Admin", false, null, "ADMINACCOUNT@GMAIL.COM", "ADMINACCOUNT@GMAIL.COM", "AQAAAAIAAYagAAAAEBu6H7C9iF9Tq3twVRr0uJaFzrtfpWgkhEKBHa0jsAD+KuMRutaglQhMbnbICcDKyQ==", null, false, "CFFCC4EEB0EE4D608E7CEFFE61FFDBD2", false, "AdminAccount@gmail.com" });

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
                    { 11, "Permissions", "Permissions.Users.ViewProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 12, "Permissions", "Permissions.Users.UpdateProfile", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" },
                    { 13, "Permissions", "Permissions.Users.ChangePassword", "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "877a5585-4894-4f4b-8989-f45476063ce1" });
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
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "877a5585-4894-4f4b-8989-f45476063ce1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9a61ca4-01bb-4a4f-8ccc-ca5dd59b42f9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "877a5585-4894-4f4b-8989-f45476063ce1");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetRoles");

           
        }
    }
}
