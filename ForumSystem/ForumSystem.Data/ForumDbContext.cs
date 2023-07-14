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
				new Like { Id = 15, UserId = 5, PostId = 5 },
				new Like { Id = 16, UserId = 6, PostId = 6 },
				new Like { Id = 17, UserId = 7, PostId = 7 },
				new Like { Id = 18, UserId = 8, PostId = 8 },
				new Like { Id = 19, UserId = 9, PostId = 9 },
				new Like { Id = 20, UserId = 10, PostId = 10 },
				new Like { Id = 21, UserId = 11, PostId = 1 },
				new Like { Id = 22, UserId = 12, PostId = 2 },
				new Like { Id = 23, UserId = 13, PostId = 3 },
				new Like { Id = 24, UserId = 14, PostId = 4 },
				new Like { Id = 25, UserId = 15, PostId = 5 }
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
