using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "877a5585-4894-4f4b-8989-f45476063ce1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "877a5585-4894-4f4b-8989-f45476063ce1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Governorate", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "877a5585-4894-4f4b-8989-f45476063ce1", 0, null, "a25eba83-ab00-408b-8dfa-ce8a1cf37bea", "AdminAccount@gmail.com", true, "Admin", null, false, "Account", false, null, "ADMINACCOUNT@GMAIL.COM", "ADMINACCOUNT@GMAIL.COM", "AQAAAAIAAYagAAAAEIoxWb26cTzdfCumxCiMdXzQAS4fntJbgyxCwE8JfNYexufyjyYGQL4kmp0ydKXiJA==", null, false, null, "CFFCC4EEB0EE4D608E7CEFFE61FFDBD2", false, "AdminAccount@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3a6ce7a1-2b66-48dd-ba28-3cf7080a3297", "877a5585-4894-4f4b-8989-f45476063ce1" });
        }
    }
}
