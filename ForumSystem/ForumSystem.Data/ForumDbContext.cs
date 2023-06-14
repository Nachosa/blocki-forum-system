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
            // Configure unique constraint for user liking a post
            builder.Entity<Like>()
                .HasIndex(l => new { l.UserId, l.PostId, l.CommentId })
                .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

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

            base.OnModelCreating(builder);

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
            builder.Entity<Role>().HasData(roles);
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
                Email = "gosho@gmail.com",
                Password = "MTIzNDU2Nzg5MA==",
                RoleId = 2

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
                RoleId = 2

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
                RoleId = 2

                //1234567890
            },
            new User()
            {
                Id = 6,
                FirstName = "Admin",
                LastName = "Adminov",
                Username = "Admin",
                Email = "Admin@gmail.com",
                Password = "MTIzNDU2Nzg5MA==",
                RoleId = 3

                //1234567890
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


            builder.Entity<User>().HasData(users);
            builder.Entity<Post>().HasData(posts);
            builder.Entity<Comment>().HasData(comments);

            IList<Like> likes = new List<Like>
            {
                new Like
                {
                    Id=1,
                    UserId = 3,
                    PostId=2,
                },

                new Like
                {
                    Id=2,
                    UserId = 2,
                    PostId=2,
                },

                new Like
                {
                    Id=3,
                    UserId = 1,
                    PostId=2,
                },

                new Like
                {
                    Id=4,
                    UserId = 1,
                    PostId=4,
                },
            };

            builder.Entity<Like>().HasData(likes);

        }


        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

    }
}
