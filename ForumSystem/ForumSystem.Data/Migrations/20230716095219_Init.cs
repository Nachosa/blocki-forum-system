using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    ProfilePicPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastName", "Password", "PhoneNumber", "ProfilePicPath", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3284), null, "gosho@gmail.com", "Gosho", false, "Goshev", "MTIzNDU2Nzg5MA==", null, null, 2, "goshoXx123" },
                    { 2, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3329), null, "Nikolai@gmail.com", "Nikolai", false, "Barekov", "MTIzNDU2Nzg5MA==", null, null, 2, "BarekaXx123" },
                    { 3, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3333), null, "boko@gmail.com", "Boiko", false, "Borisov", "MTIzNDU2Nzg5MA==", null, "/Images/UserProfilePics/936ef196-0027-4354-871e-f3fc81091f4e_00db5daec132b86843bc6692df3d369b.jpg", 2, "BokoMoko" },
                    { 4, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3337), null, "Cvetan@gmail.com", "Cvetan", false, "Cvetanov", "MTIzNDU2Nzg5MA==", null, "/Images/UserProfilePics/17c63a8c-e0d2-419a-adf7-aa329feafc81_cvetan-cvetanov-pred-fakti-peevski-i-borisov-sa-partnyori-vav-vazdeistvieto-varhu-sadebnata-sistema-1.jpg", 2, "Cvete123" },
                    { 5, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3347), null, "Kopeikin@gmail.com", "Kosta", false, "Kopeikin", "MTIzNDU2Nzg5MA==", null, "/Images/UserProfilePics/2b643649-b165-49e2-889a-3c2112e88ed7_0414338001637076422_1701823_920x708.jpeg", 2, "BrainDamage123" },
                    { 6, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3352), null, "Admin@gmail.com", "Admin", false, "Adminov", "MTIz", null, "/Images/UserProfilePics/35c6a7f8-decb-440c-853e-d32b5d0a3c64_3853-136116.jpg", 3, "Admin" },
                    { 7, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3355), null, "Andrea@gmail.com", "Andrea", false, "Paynera", "MTIzNDU2Nzg5MA==", null, "/Images/UserProfilePics/1a630573-6728-4ac3-abd1-5acf7aebb02c_Image_13437876_40_0.jpg", 2, "TopAndreika" },
                    { 8, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3359), null, "Emanuela@gmail.com", "Emanuela", false, "Paynera", "MTIzNDU2Nzg5MA==", null, null, 2, "TopEmanuelka" },
                    { 9, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3363), null, "Katrin@gmail.com", "Katrin", false, "lilova", "MTIz", null, null, 2, "Katrin" },
                    { 10, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3368), null, "Nachosa@gmail.com", "Atanas", false, "Iliev", "MTIz", null, "/Images/UserProfilePics/70f63493-d80b-44ed-a8cc-36e8b84b140c_photo.jpeg", 2, "Nachosa" },
                    { 11, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3371), null, "Gigov@gmail.com", "Nikolai", false, "Gigov", "MTIz", null, null, 2, "Nikolai" },
                    { 12, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3374), null, "Vlad@gmail.com", "Vlado", false, "Vladov", "MTIz", null, null, 2, "BatVlad" },
                    { 13, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3378), null, "Vanko@gmail.com", "Ivan", false, "Vanov", "MTIz", null, "/Images/UserProfilePics/6c456879-135e-482b-9ba9-bdbda1e6fe8e_309988-profileavatar.jpeg", 2, "BatVanko" },
                    { 14, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3382), null, "Peshaka@gmail.com", "Petar", false, "Ivanov", "MTIz", null, null, 2, "Peshaka" },
                    { 15, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3385), null, "Gergi@gmail.com", "Georgi", false, "Goshev", "MTIz", null, "/Images/UserProfilePics/c372cf2e-0cab-43a0-81e1-73ea610f9dfd_ddh0598-18d7e667-d117-4b11-8ef0-244eb60bfa45.jpg", 2, "BatGergi" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreatedOn", "DeletedOn", "IsDeleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "A non-convertible currency is one that one cannot exchange that currency on the international foreign exchange market. Outside the country, this currency has no value — it may also be referred to as locked money. For example, the Indian rupee is a semi-non convertible currency outside of India while dollars can be exchanged in all countries around the world.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3391), null, false, "WILL BITCOIN BE USED BY THE ENTIRE WORLD?", 2 },
                    { 2, "If governments decide to opt for a non-convertible currency, it is mainly to prevent capital flight abroad. In effect, by preventing convertibility, residents are then \"forced\" to use the currency in the country. Although the currency cannot leave the territory, it is nevertheless possible via complex financial instruments such as non-deliverable forwards (NDFs).", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3397), null, false, "WHY DO SOME COUNTRIES OPT FOR NON-CONVERTIBLE CURRENCIES?", 3 },
                    { 3, "Since then, the idea of ​​a single currency or a return to the gold standard has been put back on the table. It’s not a new idea, actually.\r\n\r\nDuring the Bretton Woods agreement, John Mayard Keynes proposed the creation of an international currency called the bancor, fixed by a basket of strong currencies of industrialized countries. His proposal was not accepted but his idea has continued across generations of economists.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3399), null, false, "THE IDEA OF ​​A SINGLE WORLD CURRENCY.", 4 },
                    { 4, "f there were no more national currencies, foreign exchange market-based problems and conversion fees would end immediately. Countries would no longer have a monetary barrier and could trade more freely. This would improve and increase international trade. All nations would benefit, especially countries with fragile currencies because there would be no more exchange risk.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3403), null, false, "WHAT WOULD BE THE BENEFITS OF A WORLD CURRENCY?", 5 },
                    { 5, "The world’s reserve currency must have a central authority, like the US Federal Reserve, regulating the USD’s supply and usage in global economies. However, Bitcoin is a decentralized currency without any central entity. Instead, it runs on a decentralized blockchain network that validates transactions and mints new coins based on the Bitcoin protocol.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3406), null, false, "No Central Authority ", 3 },
                    { 6, "Monero was launched in 2014 as a privacy-focused coin and, according to many, still offers the greatest degree of transaction anonymity compared to other cryptocurrencies.\r\n\r\nAlthough the Monero blockchain is open-source and operates as a decentralized, public network, all transaction details, including sender and recipient addresses and amounts, are cloaked. Monero achieves this using a combination of ring signatures and stealth addresses.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3410), null, false, "Monero", 10 },
                    { 7, "Zcash was created in 2014 as a fork of the Bitcoin code named Zerocash, which was conceived with privacy in mind. The development was later taken over by the Electric Coin Company in 2016, which still maintains Zcash to this day.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3413), null, false, "Zcash", 11 },
                    { 8, "Dash allows users to implement private transactions via a feature called PrivateSend, which cloaks transaction details. The project was started in 2014 under the name Darkcoin, but rebranded to Dash with a focus on payments in 2015.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3416), null, false, "Dash", 12 },
                    { 9, "Privacy coins are often viewed as higher-risk assets by the international Financial Action Task Force (FATF) and by national AML authorities. Some jurisdictions, such as Dubai, outlaw the use of privacy coins entirely.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3418), null, false, "Privacy Coins and Regulation", 13 },
                    { 10, "Privacy coins like Monero have no transaction history associated with them, making them more fungible than currencies like Bitcoin. Although BTC is generally considered to be a fungible asset, Bitcoin’s UTXO model means that it’s possible to trace the history of all BTC back to the point it was mined.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3422), null, false, "Benefits and Risks of Using Privacy Coins", 14 },
                    { 11, "Investing in cryptocurrency is like playing the lottery, except the odds are even worse and the prize is imaginary internet money. Trust me, I've learned the hard way.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3427), null, false, "The Risk of Investing in Cryptocurrency: A Cautionary Tale", 13 },
                    { 12, "The cryptocurrency market is like a rollercoaster ride, except instead of screaming in excitement, you're screaming in terror as your life savings plummet in value. Buckle up, folks.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3430), null, false, "Cryptocurrency: A Rollercoaster Ride of Emotions", 14 },
                    { 13, "Scammers love cryptocurrency almost as much as they love taking money from gullible investors. Always do your research before investing, or you might end up like me: broke and embarrassed.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3433), null, false, "Crypto Scams: A Fool and Their Money are Soon Parted", 15 },
                    { 14, "Hackers love cryptocurrency almost as much as they love stealing it from unsuspecting investors. Protect your investments, or you might end up like me: refreshing your empty wallet and crying into your keyboard.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3436), null, false, "Hackers and Cryptocurrency: A Match Made in Cybercrime Heaven", 7 },
                    { 15, "The regulatory environment for cryptocurrency is like a game of cat and mouse, except the cat is a government agency and the mouse is your life savings. Good luck out there.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3438), null, false, "Cryptocurrency and Regulation: A Game of Cat and Mouse", 13 },
                    { 16, "Market manipulation in the cryptocurrency world is like a game of musical chairs, except when the music stops, you're left standing without a chair or your life savings. Don't be like me, folks.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3441), null, false, "Market Manipulation and Cryptocurrency: A Tale of Boom and Bust", 14 },
                    { 17, "If a cryptocurrency exchange goes bankrupt, it's like a game of hot potato, except the potato is your life savings and it just exploded in your hands. Choose your exchange wisely, my friends.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3445), null, false, "Crypto Exchange Insolvency: When the Music Stops", 15 },
                    { 18, "Technical failures in cryptocurrency are like a computer virus, except instead of deleting your files, it deletes your life savings. Keep those updates coming, developers.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3449), null, false, "Technical Failures in Cryptocurrency: When the Code Hits the Fan", 7 },
                    { 19, "Investing in cryptocurrency without understanding the technology or market is like trying to bake a cake without a recipe. You might get lucky, but chances are you'll end up with a burnt mess and a lot of regret.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3452), null, false, "Uninformed Investing in Cryptocurrency: A Recipe for Disaster", 13 },
                    { 20, "The cryptocurrency market is highly competitive, with new coins and technologies emerging regularly. I've seen this competition impact the value of my existing coins and result in financial losses.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3425), null, false, "The Impact of Competition on My Cryptocurrency Investments", 14 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3841), null, false, "investments", 1 },
                    { 2, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3845), null, false, "boolish", 2 },
                    { 3, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3847), null, false, "future", 10 },
                    { 4, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3850), null, false, "safe", 10 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "DeletedOn", "IsDeleted", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "Bitcon is the best!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3455), null, false, 1, 2 },
                    { 2, "Bitcoin is trash", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3468), null, false, 2, 3 },
                    { 3, "Ethereum is better", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3471), null, false, 3, 3 },
                    { 4, "Ripple is the new best crypto", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3473), null, false, 4, 4 },
                    { 5, "I totally agree! Investing in cryptocurrency is like playing the lottery, except with worse odds!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3475), null, false, 11, 1 },
                    { 6, "Haha, so true! The cryptocurrency market is definitely a rollercoaster ride of emotions!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3479), null, false, 12, 2 },
                    { 7, "I've been scammed before too. It's so important to do your research before investing in cryptocurrency!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3481), null, false, 13, 3 },
                    { 8, "I've had my coins stolen by hackers before. It's so important to take security measures to protect your investments!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3483), null, false, 14, 4 },
                    { 9, "The regulatory environment for cryptocurrency is definitely uncertain. It's important to stay informed and be prepared for changes.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3486), null, false, 15, 5 },
                    { 10, "Market manipulation is such a big problem in the cryptocurrency world. It's important to be cautious and do your research before investing.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3490), null, false, 16, 6 },
                    { 11, "I've had an exchange I used go bankrupt before. It's so important to choose a reputable exchange and diversify your investments!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3493), null, false, 17, 7 },
                    { 12, "Technical failures can definitely cause problems for cryptocurrency investors. It's important to keep up to date with developments and updates.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3496), null, false, 18, 8 },
                    { 13, "I've made some risky investments in cryptocurrency before because I didn't fully understand the technology or market. Educating yourself is so important!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3498), null, false, 19, 9 },
                    { 14, "The competition in the cryptocurrency market is intense! It's important to stay informed and make smart investment decisions.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3501), null, false, 20, 10 },
                    { 15, "I totally agree! Investing in cryptocurrency can be a high-risk venture, but it can also be very rewarding if you do your research and make smart decisions.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3503), null, false, 11, 11 },
                    { 16, "The volatility of the cryptocurrency market can definitely be nerve-wracking, but it can also provide opportunities for savvy investors.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3506), null, false, 12, 12 },
                    { 17, "Crypto scams are the worst! I've lost money before because I didn't do my research. Always be cautious when investing in cryptocurrency.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3508), null, false, 13, 13 },
                    { 18, "Hackers are definitely a big threat to cryptocurrency investors. It's so important to take security measures to protect your investments!", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3512), null, false, 14, 14 },
                    { 19, "The regulatory environment for cryptocurrency is always changing. It's important to stay informed and be prepared for any changes that may come.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3514), null, false, 15, 15 },
                    { 20, "Market manipulation is such a big problem in the cryptocurrency world. Always do your research and be cautious when investing.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3517), null, false, 16, 1 },
                    { 21, "Choosing a reputable exchange is so important! I've had friends lose money because they used an exchange that went bankrupt.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3519), null, false, 17, 2 },
                    { 22, "Technical failures can definitely cause problems for investors. Keeping up to date with developments and updates is crucial.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3521), null, false, 18, 3 },
                    { 23, "Educating yourself before investing in cryptocurrency is so important! I've made some risky investments in the past because I didn't fully understand the market.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3675), null, false, 19, 4 },
                    { 24, "Competition in the cryptocurrency market is fierce! Staying informed and making smart investment decisions is key.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3678), null, false, 20, 5 },
                    { 25, "I totally agree! Investing in cryptocurrency can be risky, but it can also be very rewarding if you do your research and make smart decisions.", new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3680), null, false, 11, 6 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "CommentId", "CreatedOn", "DeletedOn", "IsDeleted", "IsDislike", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3683), null, false, false, 1, 1 },
                    { 2, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3686), null, false, false, 2, 1 },
                    { 3, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3689), null, false, false, 3, 1 },
                    { 4, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3691), null, false, false, 4, 1 },
                    { 5, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3694), null, false, false, 5, 1 },
                    { 6, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3697), null, false, false, 6, 1 },
                    { 7, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3699), null, false, false, 7, 1 },
                    { 8, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3701), null, false, false, 8, 1 },
                    { 9, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3704), null, false, false, 9, 1 },
                    { 10, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3707), null, false, false, 10, 1 },
                    { 11, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3709), null, false, false, 2, 14 },
                    { 12, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3711), null, false, false, 2, 2 },
                    { 13, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3714), null, false, false, 3, 3 },
                    { 14, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3717), null, false, false, 4, 4 },
                    { 15, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3719), null, false, true, 5, 5 },
                    { 16, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3721), null, false, false, 6, 6 },
                    { 17, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3724), null, false, true, 7, 7 },
                    { 18, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3727), null, false, false, 8, 8 },
                    { 19, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3729), null, false, false, 9, 9 },
                    { 20, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3732), null, false, false, 10, 10 },
                    { 21, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3734), null, false, true, 1, 11 },
                    { 22, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3736), null, false, false, 2, 12 },
                    { 23, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3739), null, false, true, 3, 13 },
                    { 24, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3741), null, false, false, 4, 14 },
                    { 25, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3743), null, false, false, 5, 15 },
                    { 26, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3745), null, false, false, 11, 7 },
                    { 27, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3748), null, false, false, 12, 8 },
                    { 28, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3750), null, false, true, 13, 9 },
                    { 29, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3752), null, false, false, 14, 10 },
                    { 30, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3754), null, false, false, 15, 11 },
                    { 31, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3757), null, false, true, 16, 12 },
                    { 32, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3759), null, false, false, 17, 13 },
                    { 33, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3761), null, false, false, 18, 14 },
                    { 34, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3770), null, false, true, 19, 15 },
                    { 35, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3772), null, false, false, 20, 7 },
                    { 36, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3774), null, false, true, 11, 8 },
                    { 37, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3777), null, false, false, 12, 9 },
                    { 38, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3779), null, false, false, 13, 10 },
                    { 39, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3781), null, false, false, 14, 11 },
                    { 40, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3784), null, false, true, 15, 12 },
                    { 41, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3786), null, false, false, 16, 13 },
                    { 42, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3789), null, false, true, 17, 14 },
                    { 43, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3791), null, false, false, 18, 15 },
                    { 44, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3793), null, false, true, 19, 7 },
                    { 45, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3795), null, false, false, 20, 8 },
                    { 46, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3798), null, false, false, 11, 9 },
                    { 47, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3800), null, false, false, 12, 10 },
                    { 48, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3802), null, false, false, 13, 11 },
                    { 49, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3805), null, false, true, 14, 12 },
                    { 50, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3807), null, false, false, 15, 13 },
                    { 51, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3809), null, false, true, 16, 14 },
                    { 52, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3812), null, false, true, 17, 15 },
                    { 53, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3815), null, false, false, 18, 7 },
                    { 54, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3817), null, false, false, 19, 8 },
                    { 55, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3819), null, false, false, 20, 9 },
                    { 56, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3822), null, false, true, 11, 10 },
                    { 57, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3824), null, false, false, 12, 11 },
                    { 58, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3827), null, false, false, 13, 12 },
                    { 59, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3829), null, false, false, 14, 13 },
                    { 60, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3831), null, false, true, 15, 14 },
                    { 61, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3834), null, false, false, 16, 15 },
                    { 62, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3836), null, false, false, 17, 7 },
                    { 63, null, new DateTime(2023, 7, 16, 12, 52, 19, 141, DateTimeKind.Local).AddTicks(3838), null, false, true, 18, 8 }
                });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "PostId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 2, 1, 1 },
                    { 3, 2, 3 },
                    { 4, 3, 3 },
                    { 5, 4, 1 },
                    { 6, 5, 2 },
                    { 7, 6, 3 },
                    { 8, 7, 4 },
                    { 9, 8, 1 },
                    { 10, 9, 2 },
                    { 11, 10, 3 },
                    { 12, 11, 4 },
                    { 13, 12, 1 },
                    { 14, 13, 2 },
                    { 15, 14, 3 },
                    { 16, 15, 4 },
                    { 17, 16, 1 },
                    { 18, 17, 2 },
                    { 19, 18, 3 },
                    { 20, 19, 4 }
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
