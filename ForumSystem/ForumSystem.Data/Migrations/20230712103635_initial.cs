using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    IsDislike = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Blocked" },
                    { 2, "User" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4049), null, "gosho@gmail.com", "Gosho", false, "Goshev", "MTIzNDU2Nzg5MA==", null, 2, "goshoXx123" },
                    { 2, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4267), null, "Nikolai@gmail.com", "Nikolai", false, "Barekov", "MTIzNDU2Nzg5MA==", null, 2, "BarekaXx123" },
                    { 3, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4270), null, "gosho@gmail.com", "Boiko", false, "Borisov", "MTIzNDU2Nzg5MA==", null, 2, "BokoMoko" },
                    { 4, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4274), null, "Cvetan@gmail.com", "Cvetan", false, "Cvetanov", "MTIzNDU2Nzg5MA==", null, 2, "Cvete123" },
                    { 5, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4277), null, "Kopeikin@gmail.com", "Kosta", false, "Kopeikin", "MTIzNDU2Nzg5MA==", null, 2, "BrainDamage123" },
                    { 6, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4282), null, "Admin@gmail.com", "Admin", false, "Adminov", "MTIz", null, 3, "Admin" },
                    { 7, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4285), null, "Andrea@gmail.com", "Andrea", false, "Paynera", "MTIzNDU2Nzg5MA==", null, 2, "TopAndreika" },
                    { 8, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4288), null, "Emanuela@gmail.com", "Emanuela", false, "Paynera", "MTIzNDU2Nzg5MA==", null, 2, "TopEmanuelka" },
                    { 9, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4472), null, "Katrin@gmail.com", "Katrin", false, "lilova", "MTIz", null, 2, "Katrin" },
                    { 10, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4479), null, "Nachosa@gmail.com", "Atanas", false, "Iliev", "MTIz", null, 2, "Nachosa" },
                    { 11, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4482), null, "Gigov@gmail.com", "Nikolai", false, "Gigov", "MTIz", null, 2, "Nikolai" },
                    { 12, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4484), null, "Vlad@gmail.com", "Vlado", false, "Vladov", "MTIz", null, 2, "BatVlad" },
                    { 13, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4487), null, "Vanko@gmail.com", "Ivan", false, "Vanov", "MTIz", null, 2, "BatVanko" },
                    { 14, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4490), null, "Peshaka@gmail.com", "Petar", false, "Ivanov", "MTIz", null, 2, "Peshaka" },
                    { 15, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4493), null, "Gergi@gmail.com", "Georgi", false, "Goshev", "MTIz", null, 2, "BatGergi" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreatedOn", "DeletedOn", "IsDeleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "A non-convertible currency is one that one cannot exchange that currency on the international foreign exchange market. Outside the country, this currency has no value — it may also be referred to as locked money. For example, the Indian rupee is a semi-non convertible currency outside of India while dollars can be exchanged in all countries around the world.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4498), null, false, "WILL BITCOIN BE USED BY THE ENTIRE WORLD?", 2 },
                    { 2, "If governments decide to opt for a non-convertible currency, it is mainly to prevent capital flight abroad. In effect, by preventing convertibility, residents are then \"forced\" to use the currency in the country. Although the currency cannot leave the territory, it is nevertheless possible via complex financial instruments such as non-deliverable forwards (NDFs).", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4503), null, false, "WHY DO SOME COUNTRIES OPT FOR NON-CONVERTIBLE CURRENCIES?", 3 },
                    { 3, "Since then, the idea of ​​a single currency or a return to the gold standard has been put back on the table. It’s not a new idea, actually.\r\n\r\nDuring the Bretton Woods agreement, John Mayard Keynes proposed the creation of an international currency called the bancor, fixed by a basket of strong currencies of industrialized countries. His proposal was not accepted but his idea has continued across generations of economists.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4505), null, false, "THE IDEA OF ​​A SINGLE WORLD CURRENCY.", 4 },
                    { 4, "f there were no more national currencies, foreign exchange market-based problems and conversion fees would end immediately. Countries would no longer have a monetary barrier and could trade more freely. This would improve and increase international trade. All nations would benefit, especially countries with fragile currencies because there would be no more exchange risk.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4508), null, false, "WHAT WOULD BE THE BENEFITS OF A WORLD CURRENCY?", 5 },
                    { 5, "The world’s reserve currency must have a central authority, like the US Federal Reserve, regulating the USD’s supply and usage in global economies. However, Bitcoin is a decentralized currency without any central entity. Instead, it runs on a decentralized blockchain network that validates transactions and mints new coins based on the Bitcoin protocol.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4510), null, false, "No Central Authority ", 3 },
                    { 6, "Monero was launched in 2014 as a privacy-focused coin and, according to many, still offers the greatest degree of transaction anonymity compared to other cryptocurrencies.\r\n\r\nAlthough the Monero blockchain is open-source and operates as a decentralized, public network, all transaction details, including sender and recipient addresses and amounts, are cloaked. Monero achieves this using a combination of ring signatures and stealth addresses.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4513), null, false, "Monero", 10 },
                    { 7, "Zcash was created in 2014 as a fork of the Bitcoin code named Zerocash, which was conceived with privacy in mind. The development was later taken over by the Electric Coin Company in 2016, which still maintains Zcash to this day.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4515), null, false, "Zcash", 11 },
                    { 8, "Dash allows users to implement private transactions via a feature called PrivateSend, which cloaks transaction details. The project was started in 2014 under the name Darkcoin, but rebranded to Dash with a focus on payments in 2015.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4518), null, false, "Dash", 12 },
                    { 9, "Privacy coins are often viewed as higher-risk assets by the international Financial Action Task Force (FATF) and by national AML authorities. Some jurisdictions, such as Dubai, outlaw the use of privacy coins entirely.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4520), null, false, "Privacy Coins and Regulation", 13 },
                    { 10, "Privacy coins like Monero have no transaction history associated with them, making them more fungible than currencies like Bitcoin. Although BTC is generally considered to be a fungible asset, Bitcoin’s UTXO model means that it’s possible to trace the history of all BTC back to the point it was mined.", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4523), null, false, "Benefits and Risks of Using Privacy Coins", 14 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4547), null, false, "investments", 1 },
                    { 2, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4550), null, false, "boolish", 2 },
                    { 3, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4552), null, false, "future", 10 },
                    { 4, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4554), null, false, "safe", 10 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "DeletedOn", "IsDeleted", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "Bitcon is the best!", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4527), null, false, 1, 2 },
                    { 2, "Bitcoin is trash", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4531), null, false, 2, 3 },
                    { 3, "Ethereum is better", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4533), null, false, 3, 3 },
                    { 4, "Ripple is the new best crypto", new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4535), null, false, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "CommentId", "CreatedOn", "DeletedOn", "IsDeleted", "IsDislike", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4537), null, false, false, 2, 3 },
                    { 2, null, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4541), null, false, false, 2, 2 },
                    { 3, null, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4543), null, false, false, 2, 1 },
                    { 4, null, new DateTime(2023, 7, 12, 13, 36, 35, 425, DateTimeKind.Local).AddTicks(4545), null, false, false, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "PostId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 2, 1, 1 },
                    { 3, 2, 3 },
                    { 4, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId_PostId_CommentId",
                table: "Likes",
                columns: new[] { "UserId", "PostId", "CommentId" },
                unique: true,
                filter: "[PostId] IS NOT NULL AND [CommentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
