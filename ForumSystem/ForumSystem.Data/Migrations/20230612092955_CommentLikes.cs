using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CommentLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserId_PostId",
                table: "Like");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Like",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId_PostId_CommentId",
                table: "Like",
                columns: new[] { "UserId", "PostId", "CommentId" },
                unique: true,
                filter: "[PostId] IS NOT NULL AND [CommentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserId_PostId_CommentId",
                table: "Like");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Like",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId_PostId",
                table: "Like",
                columns: new[] { "UserId", "PostId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
