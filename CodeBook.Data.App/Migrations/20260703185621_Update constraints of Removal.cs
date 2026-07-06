using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBook.Data.App.Migrations
{
    /// <inheritdoc />
    public partial class UpdateconstraintsofRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Post_Removal_RemoverId_PostId",
                table: "Post_Removal");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Removal_RemoverId_CommentId",
                table: "Comment_Removal");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Removal_RemoverId",
                table: "Post_Removal",
                column: "RemoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Removal_RemoverId",
                table: "Comment_Removal",
                column: "RemoverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Post_Removal_RemoverId",
                table: "Post_Removal");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Removal_RemoverId",
                table: "Comment_Removal");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Removal_RemoverId_PostId",
                table: "Post_Removal",
                columns: new[] { "RemoverId", "PostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Removal_RemoverId_CommentId",
                table: "Comment_Removal",
                columns: new[] { "RemoverId", "CommentId" },
                unique: true);
        }
    }
}
