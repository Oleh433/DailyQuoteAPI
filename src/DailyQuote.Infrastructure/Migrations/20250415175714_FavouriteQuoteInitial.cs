using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyQuote.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FavouriteQuoteInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FavouriteQuotesId",
                table: "Quotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavouriteQuotesId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_FavouriteQuotesId",
                table: "Quotes",
                column: "FavouriteQuotesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AspNetUsers_FavouriteQuotesId",
                table: "Quotes",
                column: "FavouriteQuotesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AspNetUsers_FavouriteQuotesId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_FavouriteQuotesId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "FavouriteQuotesId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "FavouriteQuotesId",
                table: "AspNetUsers");
        }
    }
}
