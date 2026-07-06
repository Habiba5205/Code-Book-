using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBook.Data.App.Migrations
{
    /// <inheritdoc />
    public partial class AddsenderinNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "notifications",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "notifications");
        }
    }
}
