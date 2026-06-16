using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsStarredToChatConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStarred",
                table: "ChatConversations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStarred",
                table: "ChatConversations");
        }
    }
}
