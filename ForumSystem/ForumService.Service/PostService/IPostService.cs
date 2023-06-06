using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business
{
    public interface IPostService
    {
        public IList<Post> GetAllPosts();
    }
}
