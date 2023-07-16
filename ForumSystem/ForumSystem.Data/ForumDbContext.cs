using ForumSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ForumSystem.DataAccess
{
	public class ForumSystemContext : DbContext
	{
		public ForumSystemContext(DbContextOptions<ForumSystemContext> options)
			: base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//Вкарах ги в отделни методи за по-чисто, дано не направи проблем в бъдеще.
			ConfigureMigration(builder);

			base.OnModelCreating(builder);

			CreateSeed(builder);
		}


		public DbSet<Post> Posts { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Tag> Tags { get; set; }

		public DbSet<Like> Likes { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<PostTag> PostTags { get; set; }

		protected void ConfigureMigration(ModelBuilder builder)
		{
			// Configure unique constraint for user liking a post
			// Попринцип работи нормално дори и когато един юзър хареса, отхареса и после пак хареса същия пост, но не съм сигурен защо, все си мисля че трябва да има и Id в долната конфигурация.
			builder.Entity<Like>()
				.HasIndex(l => new { /*l.Id, */l.UserId, l.PostId, l.CommentId })
				.IsUnique();

			builder.Entity<User>()
				.HasMany(u => u.Likes)
				.WithOne(l => l.User)
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<User>()
				.HasMany(u => u.Tags)
				.WithOne(t => t.User)
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			//        builder.Entity<Tag>()
			//            .HasOne(t => t.User)
			//            .WithMany(u => u.Tags)
			//.HasForeignKey(t => t.UserId)
			//.OnDelete(DeleteBehavior.NoAction);

			// Configure Post and Like relationship
			//modelBuilder.Entity<Post>()
			//    .HasMany(p => p.Likes)
			//    .WithOne(l => l.Post)
			//    .HasForeignKey(l => l.PostId)
			//    .OnDelete(DeleteBehavior.Cascade);

			//Configure Post and Comment relationship
			//builder.Entity<Post>()
			//    .HasMany(c => c.Comments)
			//    .WithOne(p => p.Post)
			//    .HasForeignKey(p => p.PostId)
			//    .OnDelete(DeleteBehavior.Cascade);

			builder.Entity<User>()
				.HasMany(u => u.Comments)
				.WithOne(c => c.User)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Comment>()
				.HasMany(c => c.Likes)
				.WithOne(l => l.Comment)
				.HasForeignKey(l => l.CommentId)
				.OnDelete(DeleteBehavior.NoAction);
		}

		protected void CreateSeed(ModelBuilder builder)
		{
			IList<Role> roles = new List<Role>
			{
				new Role
				{
					Id = 1,
					Name="Blocked"
				},
				new Role
				{
					Id = 2,
					Name = "User"
				},
				new Role
				{
					Id = 3,
					Name = "Admin"
				}
			};

			IList<User> users = new List<User>
			{
			new User()
			{
				Id = 1,
				FirstName = "Gosho",
				LastName = "Goshev",
				Username = "goshoXx123",
				Email = "gosho@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2
                //1234567890
            },
			new User()
			{
				Id = 2,
				FirstName = "Nikolai",
				LastName = "Barekov",
				Username = "BarekaXx123",
				Email = "Nikolai@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2

                //1234567890

            },
			new User()
			{
				Id = 3,
				FirstName = "Boiko",
				LastName = "Borisov",
				Username = "BokoMoko",
				Email = "boko@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/936ef196-0027-4354-871e-f3fc81091f4e_00db5daec132b86843bc6692df3d369b.jpg"

                //1234567890
            },
			new User()
			{
				Id = 4,
				FirstName = "Cvetan",
				LastName = "Cvetanov",
				Username = "Cvete123",
				Email = "Cvetan@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/17c63a8c-e0d2-419a-adf7-aa329feafc81_cvetan-cvetanov-pred-fakti-peevski-i-borisov-sa-partnyori-vav-vazdeistvieto-varhu-sadebnata-sistema-1.jpg"

                //1234567890
            },
			new User()
			{
				Id = 5,
				FirstName = "Kosta",
				LastName = "Kopeikin",
				Username = "BrainDamage123",
				Email = "Kopeikin@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/2b643649-b165-49e2-889a-3c2112e88ed7_0414338001637076422_1701823_920x708.jpeg"


                //1234567890
            },
			new User()
			{
				Id = 6,
				FirstName = "Admin",
				LastName = "Adminov",
				Username = "Admin",
				Email = "Admin@gmail.com",
				Password = "MTIz",
				RoleId = 3,
				ProfilePicPath="/Images/UserProfilePics/35c6a7f8-decb-440c-853e-d32b5d0a3c64_3853-136116.jpg"


                //123
            },
			new User()
			{
				Id = 7,
				FirstName = "Andrea",
				LastName = "Paynera",
				Username = "TopAndreika",
				Email = "Andrea@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/1a630573-6728-4ac3-abd1-5acf7aebb02c_Image_13437876_40_0.jpg"

                //1234567890
            },
			new User()
			{
				Id = 8,
				FirstName = "Emanuela",
				LastName = "Paynera",
				Username = "TopEmanuelka",
				Email = "Emanuela@gmail.com",
				Password = "MTIzNDU2Nzg5MA==",
				RoleId = 2

                //1234567890
            },
			new User()
			{
				Id = 9,
				FirstName = "Katrin",
				LastName = "lilova",
				Username = "Katrin",
				Email = "Katrin@gmail.com",
				Password = "MTIz",
				RoleId = 2

                //123
            },
			new User()
			{
				Id = 10,
				FirstName = "Atanas",
				LastName = "Iliev",
				Username = "Nachosa",
				Email = "Nachosa@gmail.com",
				Password = "MTIz",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/70f63493-d80b-44ed-a8cc-36e8b84b140c_photo.jpeg"

                //123
            },
			new User()
			{
				Id = 11,
				FirstName = "Nikolai",
				LastName = "Gigov",
				Username = "Nikolai",
				Email = "Gigov@gmail.com",
				Password = "MTIz",
				RoleId = 2

                //123
            },
			new User()
			{
				Id = 12,
				FirstName = "Vlado",
				LastName = "Vladov",
				Username = "BatVlad",
				Email = "Vlad@gmail.com",
				Password = "MTIz",
				RoleId = 2

                //123
            },
			new User()
			{
				Id = 13,
				FirstName = "Ivan",
				LastName = "Vanov",
				Username = "BatVanko",
				Email = "Vanko@gmail.com",
				Password = "MTIz",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/6c456879-135e-482b-9ba9-bdbda1e6fe8e_309988-profileavatar.jpeg"

                //123
            },
			new User()
			{
				Id = 14,
				FirstName = "Petar",
				LastName = "Ivanov",
				Username = "Peshaka",
				Email = "Peshaka@gmail.com",
				Password = "MTIz",
				RoleId = 2

                //123
            },
			new User()
			{
				Id = 15,
				FirstName = "Georgi",
				LastName = "Goshev",
				Username = "BatGergi",
				Email = "Gergi@gmail.com",
				Password = "MTIz",
				RoleId = 2,
				ProfilePicPath="/Images/UserProfilePics/c372cf2e-0cab-43a0-81e1-73ea610f9dfd_ddh0598-18d7e667-d117-4b11-8ef0-244eb60bfa45.jpg"

                //123
            }
			};

			IList<Post> posts = new List<Post>
			{
				new Post
				{
					Id=1,
					UserId=2,
					Title="WILL BITCOIN BE USED BY THE ENTIRE WORLD?",
					Content="A non-convertible currency is one that one cannot exchange that currency on the international foreign exchange market. Outside the country, this currency has no value — it may also be referred to as locked money. For example, the Indian rupee is a semi-non convertible currency outside of India while dollars can be exchanged in all countries around the world."
				},
				new Post
				{
					Id=2,
					UserId=3,
					Title="WHY DO SOME COUNTRIES OPT FOR NON-CONVERTIBLE CURRENCIES?",
					Content="If governments decide to opt for a non-convertible currency, it is mainly to prevent capital flight abroad. In effect, by preventing convertibility, residents are then \"forced\" to use the currency in the country. Although the currency cannot leave the territory, it is nevertheless possible via complex financial instruments such as non-deliverable forwards (NDFs)."
				},
				new Post
				{
					Id=3,
					UserId=4,
					Title="THE IDEA OF ​​A SINGLE WORLD CURRENCY.",
					Content="Since then, the idea of ​​a single currency or a return to the gold standard has been put back on the table. It’s not a new idea, actually.\r\n\r\nDuring the Bretton Woods agreement, John Mayard Keynes proposed the creation of an international currency called the bancor, fixed by a basket of strong currencies of industrialized countries. His proposal was not accepted but his idea has continued across generations of economists."
				},
				new Post
				{
					Id=4,
					UserId=5,
					Title="WHAT WOULD BE THE BENEFITS OF A WORLD CURRENCY?",
					Content="f there were no more national currencies, foreign exchange market-based problems and conversion fees would end immediately. Countries would no longer have a monetary barrier and could trade more freely. This would improve and increase international trade. All nations would benefit, especially countries with fragile currencies because there would be no more exchange risk."

				},
				new Post
				{
					Id=5,
					UserId=3,
					Title="No Central Authority ",
					Content="The world’s reserve currency must have a central authority, like the US Federal Reserve, regulating the USD’s supply and usage in global economies. However, Bitcoin is a decentralized currency without any central entity. Instead, it runs on a decentralized blockchain network that validates transactions and mints new coins based on the Bitcoin protocol."

				},
				new Post
				{
					Id=6,
					UserId=10,
					Title="Monero",
					Content="Monero was launched in 2014 as a privacy-focused coin and, according to many, still offers the greatest degree of transaction anonymity compared to other cryptocurrencies.\r\n\r\nAlthough the Monero blockchain is open-source and operates as a decentralized, public network, all transaction details, including sender and recipient addresses and amounts, are cloaked. Monero achieves this using a combination of ring signatures and stealth addresses."
				},
				new Post
				{
					Id=7,
					UserId=11,
					Title="Zcash",
					Content="Zcash was created in 2014 as a fork of the Bitcoin code named Zerocash, which was conceived with privacy in mind. The development was later taken over by the Electric Coin Company in 2016, which still maintains Zcash to this day."

				},
				new Post
				{
					Id=8,
					UserId=12,
					Title="Dash",
					Content="Dash allows users to implement private transactions via a feature called PrivateSend, which cloaks transaction details. The project was started in 2014 under the name Darkcoin, but rebranded to Dash with a focus on payments in 2015."
				},
				new Post
				{
					Id=9,
					UserId=13,
					Title="Privacy Coins and Regulation",
					Content="Privacy coins are often viewed as higher-risk assets by the international Financial Action Task Force (FATF) and by national AML authorities. Some jurisdictions, such as Dubai, outlaw the use of privacy coins entirely."
				},
				new Post
				{
					Id=10,
					UserId=14,
					Title="Benefits and Risks of Using Privacy Coins",
					Content="Privacy coins like Monero have no transaction history associated with them, making them more fungible than currencies like Bitcoin. Although BTC is generally considered to be a fungible asset, Bitcoin’s UTXO model means that it’s possible to trace the history of all BTC back to the point it was mined."
				},
				new Post
				{
					Id = 20,
					UserId = 14,
					Title = "The Impact of Competition on My Cryptocurrency Investments",
					Content = "The cryptocurrency market is highly competitive, with new coins and technologies emerging regularly. I've seen this competition impact the value of my existing coins and result in financial losses."
				},
				new Post
				{
					Id = 11,
					UserId = 13,
					Title = "The Risk of Investing in Cryptocurrency: A Cautionary Tale",
					Content = "Investing in cryptocurrency is like playing the lottery, except the odds are even worse and the prize is imaginary internet money. Trust me, I've learned the hard way."
				},
				new Post
				{
					Id = 12,
					UserId = 14,
					Title = "Cryptocurrency: A Rollercoaster Ride of Emotions",
					Content = "The cryptocurrency market is like a rollercoaster ride, except instead of screaming in excitement, you're screaming in terror as your life savings plummet in value. Buckle up, folks."
				},
				new Post
				{
					Id = 13,
					UserId = 15,
					Title = "Crypto Scams: A Fool and Their Money are Soon Parted",
					Content = "Scammers love cryptocurrency almost as much as they love taking money from gullible investors. Always do your research before investing, or you might end up like me: broke and embarrassed."
				},
				new Post
				{
					Id = 14,
					UserId = 7,
					Title = "Hackers and Cryptocurrency: A Match Made in Cybercrime Heaven",
					Content = "Hackers love cryptocurrency almost as much as they love stealing it from unsuspecting investors. Protect your investments, or you might end up like me: refreshing your empty wallet and crying into your keyboard."
				},
				new Post
				{
					Id = 15,
					UserId = 13,
					Title = "Cryptocurrency and Regulation: A Game of Cat and Mouse",
					Content = "The regulatory environment for cryptocurrency is like a game of cat and mouse, except the cat is a government agency and the mouse is your life savings. Good luck out there."
				},
				new Post
				{
					Id = 16,
					UserId = 14,
					Title = "Market Manipulation and Cryptocurrency: A Tale of Boom and Bust",
					Content = "Market manipulation in the cryptocurrency world is like a game of musical chairs, except when the music stops, you're left standing without a chair or your life savings. Don't be like me, folks."
				},
				new Post
				{
					Id = 17,
					UserId = 15,
					Title = "Crypto Exchange Insolvency: When the Music Stops",
					Content = "If a cryptocurrency exchange goes bankrupt, it's like a game of hot potato, except the potato is your life savings and it just exploded in your hands. Choose your exchange wisely, my friends."
				},
				new Post
				{
					Id = 18,
					UserId = 7,
					Title = "Technical Failures in Cryptocurrency: When the Code Hits the Fan",
					Content = "Technical failures in cryptocurrency are like a computer virus, except instead of deleting your files, it deletes your life savings. Keep those updates coming, developers."
				},
				new Post
				{
					Id = 19,
					UserId = 13,
					Title = "Uninformed Investing in Cryptocurrency: A Recipe for Disaster",
					Content = "Investing in cryptocurrency without understanding the technology or market is like trying to bake a cake without a recipe. You might get lucky, but chances are you'll end up with a burnt mess and a lot of regret."
				}
			};

			IList<Comment> comments = new List<Comment>
			{
				new Comment
				{
					Id=1,
					Content="Bitcon is the best!",
					UserId = 2,
					PostId=1,

				},
				new Comment
				{
					Id=2,
					Content="Bitcoin is trash",
					UserId = 3,
					PostId=2,
				},
				new Comment
				{
					Id=3,
					Content="Ethereum is better",
					UserId=3,
					PostId=3,
				},
				new Comment
				{
					Id=4,
					Content="Ripple is the new best crypto",
					UserId=4,
					PostId=4,
				},
				new Comment
				{
					Id = 5,
					Content = "I totally agree! Investing in cryptocurrency is like playing the lottery, except with worse odds!",
					UserId = 1,
					PostId = 11
				},
				new Comment
				{
					Id = 6,
					Content = "Haha, so true! The cryptocurrency market is definitely a rollercoaster ride of emotions!",
					UserId = 2,
					PostId = 12
				},
				new Comment
				{
					Id = 7,
					Content = "I've been scammed before too. It's so important to do your research before investing in cryptocurrency!",
					UserId = 3,
					PostId = 13
				},
				new Comment
				{
					Id = 8,
					Content = "I've had my coins stolen by hackers before. It's so important to take security measures to protect your investments!",
					UserId = 4,
					PostId = 14
				},
				new Comment
				{
					Id = 9,
					Content = "The regulatory environment for cryptocurrency is definitely uncertain. It's important to stay informed and be prepared for changes.",
					UserId = 5,
					PostId = 15
				},
				new Comment
				{
					Id = 10,
					Content = "Market manipulation is such a big problem in the cryptocurrency world. It's important to be cautious and do your research before investing.",
					UserId = 6,
					PostId = 16
				},
				new Comment
				{
					Id = 11,
					Content = "I've had an exchange I used go bankrupt before. It's so important to choose a reputable exchange and diversify your investments!",
					UserId = 7,
					PostId = 17
				},
				new Comment
				{
					Id = 12,
					Content = "Technical failures can definitely cause problems for cryptocurrency investors. It's important to keep up to date with developments and updates.",
					UserId = 8,
					PostId = 18
				},
				new Comment
				{
					Id = 13,
					Content = "I've made some risky investments in cryptocurrency before because I didn't fully understand the technology or market. Educating yourself is so important!",
					UserId = 9,
					PostId = 19
				},
				new Comment
				{
					Id = 14,
					Content = "The competition in the cryptocurrency market is intense! It's important to stay informed and make smart investment decisions.",
					UserId = 10,
					PostId = 20
				},
				new Comment
				{
					Id = 15,
					Content = "I totally agree! Investing in cryptocurrency can be a high-risk venture, but it can also be very rewarding if you do your research and make smart decisions.",
					UserId = 11,
					PostId = 11
				},
				new Comment
				{
					Id = 16,
					Content = "The volatility of the cryptocurrency market can definitely be nerve-wracking, but it can also provide opportunities for savvy investors.",
					UserId = 12,
					PostId=12
				},
				new Comment
				{
					Id=17,
					Content="Crypto scams are the worst! I've lost money before because I didn't do my research. Always be cautious when investing in cryptocurrency.",
					UserId=13,
					PostId=13
				},
				new Comment
				{
					Id=18,
					Content="Hackers are definitely a big threat to cryptocurrency investors. It's so important to take security measures to protect your investments!",
					UserId=14,
					PostId=14
				},
				new Comment
				{
					Id=19,
					Content="The regulatory environment for cryptocurrency is always changing. It's important to stay informed and be prepared for any changes that may come.",
					UserId=15,
					PostId=15
				},
				new Comment
				{
					Id=20,
					Content="Market manipulation is such a big problem in the cryptocurrency world. Always do your research and be cautious when investing.",
					UserId=1,
					PostId=16
				},
				new Comment
				{
					Id=21,
					Content="Choosing a reputable exchange is so important! I've had friends lose money because they used an exchange that went bankrupt.",
					UserId=2,
					PostId=17
				},
				new Comment
				{
					Id=22,
					Content="Technical failures can definitely cause problems for investors. Keeping up to date with developments and updates is crucial.",
					UserId=3,
					PostId=18
				},
				new Comment
				{
					Id=23,
					Content="Educating yourself before investing in cryptocurrency is so important! I've made some risky investments in the past because I didn't fully understand the market.",
					UserId=4,
					PostId=19 },
				new Comment
				{
					Id=24,
					Content="Competition in the cryptocurrency market is fierce! Staying informed and making smart investment decisions is key.",
					UserId=5,
					PostId=20
				},
				new Comment
				{
					Id=25,
					Content="I totally agree! Investing in cryptocurrency can be risky, but it can also be very rewarding if you do your research and make smart decisions.",
					UserId=6,
					PostId=11
				}
			};

			IList<Like> likes = new List<Like>
			{
				new Like
				{
					Id=1,
					UserId = 1,
					PostId=1,
				},

				new Like
				{
					Id=2,
					UserId = 1,
					PostId=2,
				},

				new Like
				{
					Id=3,
					UserId = 1,
					PostId=3,
				},

				new Like
				{
					Id=4,
					UserId = 1,
					PostId=4,
				},
				new Like
				{
					Id=5,
					UserId = 1,
					PostId=5,
				},
				new Like
				{
					Id=6,
					UserId = 1,
					PostId=6,
				},

				new Like
				{
					Id=7,
					UserId = 1,
					PostId=7,
				},

				new Like
				{
					Id=8,
					UserId = 1,
					PostId=8,
				},

				new Like
				{
					Id=9,
					UserId = 1,
					PostId=9,
				},
				new Like
				{
					Id=10,
					UserId = 1,
					PostId=10,
				},
				new Like { Id = 11, UserId = 14, PostId = 2 },
				new Like { Id = 12, UserId = 2, PostId = 2 },
				new Like { Id = 13, UserId = 3, PostId = 3 },
				new Like { Id = 14, UserId = 4, PostId = 4 },
				new Like { Id = 15, UserId = 5, PostId = 5, IsDislike = true },
				new Like { Id = 16, UserId = 6, PostId = 6 },
				new Like { Id = 17, UserId = 7, PostId = 7, IsDislike = true },
				new Like { Id = 18, UserId = 8, PostId = 8 },
				new Like { Id = 19, UserId = 9, PostId = 9 },
				new Like { Id = 20, UserId = 10, PostId = 10 },
				new Like { Id = 21, UserId = 11, PostId = 1, IsDislike = true },
				new Like { Id = 22, UserId = 12, PostId = 2 },
				new Like { Id = 23, UserId = 13, PostId = 3, IsDislike = true },
				new Like { Id = 24, UserId = 14, PostId = 4 },
				new Like { Id = 25, UserId = 15, PostId = 5 },
				new Like { Id = 26, UserId = 7, PostId = 11 },
				new Like { Id = 27, UserId = 8, PostId = 12 },
				new Like { Id = 28, UserId = 9, PostId = 13, IsDislike = true },
				new Like { Id = 29, UserId = 10, PostId = 14 },
				new Like { Id = 30, UserId = 11, PostId = 15 },
				new Like { Id = 31, UserId = 12, PostId = 16, IsDislike = true },
				new Like { Id = 32, UserId = 13, PostId = 17 },
				new Like { Id = 33, UserId = 14, PostId = 18 },
				new Like { Id = 34, UserId = 15, PostId = 19, IsDislike = true },
				new Like { Id = 35, UserId = 7, PostId = 20 },
				new Like { Id = 36, UserId = 8, PostId = 11, IsDislike = true },
				new Like { Id = 37, UserId = 9, PostId = 12 },
				new Like { Id = 38, UserId = 10, PostId = 13 },
				new Like { Id = 39, UserId = 11, PostId=14},
				new Like{Id=40,UserId=12,PostId=15, IsDislike = true},
				new Like{Id=41,UserId=13,PostId=16},
				new Like{Id=42,UserId=14,PostId=17, IsDislike = true},
				new Like{Id=43,UserId=15,PostId=18},
				new Like{Id=44,UserId=7,PostId=19, IsDislike = true},
				new Like{Id=45,UserId=8,PostId=20},
				new Like{Id=46,UserId=9,PostId=11},
				new Like{Id=47,UserId=10,PostId=12},
				new Like{Id=48,UserId=11,PostId=13},
				new Like{Id=49,UserId=12,PostId=14, IsDislike = true},
				new Like{Id=50,UserId=13,PostId=15},
				new Like{Id=51,UserId=14,PostId=16, IsDislike = true},
				new Like{Id=52,UserId=15,PostId=17, IsDislike = true},
				new Like{Id=53,UserId=7 ,PostId=18},
				new Like{Id=54 ,UserId=8 ,PostId=19},
				new Like{ Id =55 ,UserId=9 ,PostId=20},
				new Like{ Id =56 ,UserId=10 ,PostId=11, IsDislike = true},
				new Like{ Id =57 ,UserId=11 ,PostId=12},
				new Like{ Id =58 ,UserId=12 ,PostId=13},
				new Like{ Id =59 ,UserId=13 ,PostId=14},
				new Like{ Id =60 ,UserId=14 ,PostId=15, IsDislike = true},
				new Like{ Id =61 ,UserId=15 ,PostId=16},
				new Like{ Id =62 ,UserId=7 ,PostId=17},
				new Like{ Id =63 ,UserId=8 ,PostId=18, IsDislike = true }
			};

			IList<Tag> tags = new List<Tag>
			{
				new Tag
				{
					Id = 1,
					Name = "investments",
					UserId=1
				},

				new Tag
				{
					Id = 2,
					Name = "boolish",
					UserId=2
				},

				new Tag
				{
					Id = 3,
					Name = "future",
					UserId=10
				},

				new Tag
				{
					Id = 4,
					Name = "safe",
					UserId=10
				},
			};

			IList<PostTag> postTags = new List<PostTag>
			{
				new PostTag
				{
					Id=1,
					PostId=1,
					TagId=3
				},
				new PostTag
				{
					Id=2,
					PostId=1,
					TagId=1
				},
				new PostTag
				{
					Id=3,
					PostId=2,
					TagId=3
				},
				new PostTag
				{
					Id=4,
					PostId=3,
					TagId=3
				},
				new PostTag { Id = 5, PostId = 4, TagId = 1 },
				new PostTag { Id = 6, PostId = 5, TagId = 2 },
				new PostTag { Id = 7, PostId = 6, TagId = 3 },
				new PostTag { Id = 8, PostId = 7, TagId = 4 },
				new PostTag { Id = 9, PostId = 8, TagId = 1 },
				new PostTag { Id = 10, PostId = 9, TagId = 2 },
				new PostTag { Id = 11, PostId = 10, TagId = 3 },
				new PostTag { Id = 12, PostId = 11, TagId = 4 },
				new PostTag { Id = 13, PostId = 12, TagId = 1 },
				new PostTag { Id = 14, PostId=13, TagId=2},
				new PostTag{ Id=15, PostId=14, TagId=3},
				new PostTag{ Id=16, PostId=15, TagId=4},
				new PostTag{ Id=17, PostId=16, TagId=1},
				new PostTag{ Id=18, PostId=17, TagId=2},
				new PostTag{ Id=19, PostId=18, TagId=3},
				new PostTag{ Id=20, PostId=19, TagId=4}
			};

			builder.Entity<Role>().HasData(roles);
			builder.Entity<User>().HasData(users);
			builder.Entity<Post>().HasData(posts);
			builder.Entity<Comment>().HasData(comments);
			builder.Entity<Like>().HasData(likes);
			builder.Entity<Tag>().HasData(tags);
			builder.Entity<PostTag>().HasData(postTags);

		}
	}
}
