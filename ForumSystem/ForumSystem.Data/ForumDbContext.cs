using ForumSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess
{
    public class ForumSystemContext : DbContext
    {
        public ForumSystemContext(DbContextOptions<ForumSystemContext> options) 
            : base (options)
        { 

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
