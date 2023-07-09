using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7626));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7631));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7552));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7596));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7599));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7604));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7615));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7619));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7468));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7511));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7514));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7516));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7521));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7523));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7529));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7535));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7538));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7541));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 16, 20, 11, 708, DateTimeKind.Local).AddTicks(7546));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1121));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1127));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1130));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1135));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1144));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1085));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1091));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1097));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1100));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1104));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1107));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1110));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1113));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1117));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(989));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1027));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1032));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1035));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1038));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1042));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1045));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1052));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1057));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1060));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1063));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1066));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1069));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 9, 12, 2, 54, 905, DateTimeKind.Local).AddTicks(1080));
        }
    }
}
