using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.Business.Abstractions;
using ForumSystem.Business.Models;

namespace ForumSystem.Business
{
    public class PostService : IPostService
    {
        private readonly IForumSystemRepository repo;

        public PostService (IForumSystemRepository repo) 
        {
            this.repo = repo;
        }

        public IList<Post> GetAllPosts()
        {
            return this.repo.GetAllPosts().ToList();
        }
    }
}
