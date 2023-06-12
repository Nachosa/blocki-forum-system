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
            }
            };

            builder.Entity<User>().HasData(users);
            
        }


        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Role> Roles { get; set; }

        //public DbSet<PostTag> Tags { get; set; }

    }
}
