using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess;

public interface IForumSystemRepository
{
    public IEnumerable<Post> GetAllPosts();

    public Post CreatePost(Post post);
}
