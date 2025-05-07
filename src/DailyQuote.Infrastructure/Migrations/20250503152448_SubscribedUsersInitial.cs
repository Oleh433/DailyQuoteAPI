using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyQuote.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscribedUsersInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveQuotes",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "SubscribedUsers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedUsers", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscribedUsers");

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveQuotes",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
