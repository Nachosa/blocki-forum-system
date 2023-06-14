using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LikesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5453));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5463));

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "CommentId", "CreatedOn", "DeletedOn", "IsDeleted", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5516), null, false, 2, 3 },
                    { 2, null, new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5520), null, false, 2, 2 },
                    { 3, null, new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5523), null, false, 2, 1 },
                    { 4, null, new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5525), null, false, 4, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5440));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5446));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5449));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5378));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5414));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5421));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 14, 12, 5, 52, 798, DateTimeKind.Local).AddTicks(5430));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1491));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1495));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1466));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1471));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1483));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1486));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1455));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 13, 19, 57, 48, 947, DateTimeKind.Local).AddTicks(1460));
        }
    }
}
