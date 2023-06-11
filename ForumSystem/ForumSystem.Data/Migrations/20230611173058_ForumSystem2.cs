using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ForumSystem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Comments_CommentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Like",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentId",
                table: "Like",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Comments_CommentId",
                table: "Like",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Comments_CommentId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_CommentId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Like");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommentId",
                table: "Users",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Comments_CommentId",
                table: "Users",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
