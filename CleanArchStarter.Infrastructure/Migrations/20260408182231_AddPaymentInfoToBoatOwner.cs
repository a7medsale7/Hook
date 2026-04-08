using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentInfoToBoatOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstaPayNumber",
                table: "BoatOwnerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VodafoneCashNumber",
                table: "BoatOwnerProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstaPayNumber",
                table: "BoatOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "VodafoneCashNumber",
                table: "BoatOwnerProfiles");
        }
    }
}
