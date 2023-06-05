using ForumSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.Abstractions
{
    public interface IPostService
    {
        public IList<Post> GetAllPosts();
    }
}
