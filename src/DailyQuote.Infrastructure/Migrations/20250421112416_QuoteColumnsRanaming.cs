using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyQuote.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuoteColumnsRanaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AspNetUsers_FavouriteQuotesId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "QuoteContent",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "QuoteType",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "FavouriteQuotesId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FavouriteQuotesId",
                table: "Quotes",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "QuoteId",
                table: "Quotes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_FavouriteQuotesId",
                table: "Quotes",
                newName: "IX_Quotes_ApplicationUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastShownTime",
                table: "Quotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AspNetUsers_ApplicationUserId",
                table: "Quotes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AspNetUsers_ApplicationUserId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Quotes",
                newName: "FavouriteQuotesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Quotes",
                newName: "QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_ApplicationUserId",
                table: "Quotes",
                newName: "IX_Quotes_FavouriteQuotesId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastShownTime",
                table: "Quotes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "QuoteContent",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuoteType",
                table: "Quotes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavouriteQuotesId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AspNetUsers_FavouriteQuotesId",
                table: "Quotes",
                column: "FavouriteQuotesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
