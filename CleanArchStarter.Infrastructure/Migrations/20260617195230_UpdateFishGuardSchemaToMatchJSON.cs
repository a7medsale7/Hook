using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFishGuardSchemaToMatchJSON : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "RestrictedTools");

            migrationBuilder.RenameColumn(
                name: "ToolName",
                table: "RestrictedTools",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "RestrictedTools",
                newName: "Material");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "FishingSeasons",
                newName: "SeasonName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "FishingSeasons",
                newName: "RestrictedFishSpecies");

            migrationBuilder.AddColumn<string>(
                name: "BanReason",
                table: "RestrictedTools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MaxLengthMeters",
                table: "RestrictedTools",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinMeshSizeCm",
                table: "RestrictedTools",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "RestrictedTools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BannedTools",
                table: "FishingSeasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<bool>(
                name: "IsStrictlyEnforced",
                table: "FishingSeasons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "FishingSeasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "FishingSeasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanReason",
                table: "RestrictedTools");

            migrationBuilder.DropColumn(
                name: "MaxLengthMeters",
                table: "RestrictedTools");

            migrationBuilder.DropColumn(
                name: "MinMeshSizeCm",
                table: "RestrictedTools");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "RestrictedTools");

            migrationBuilder.DropColumn(
                name: "BannedTools",
                table: "FishingSeasons");

            migrationBuilder.DropColumn(
                name: "IsStrictlyEnforced",
                table: "FishingSeasons");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "FishingSeasons");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "FishingSeasons");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RestrictedTools",
                newName: "ToolName");

            migrationBuilder.RenameColumn(
                name: "Material",
                table: "RestrictedTools",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "SeasonName",
                table: "FishingSeasons",
                newName: "Species");

            migrationBuilder.RenameColumn(
                name: "RestrictedFishSpecies",
                table: "FishingSeasons",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Penalty",
                table: "RestrictedTools",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
