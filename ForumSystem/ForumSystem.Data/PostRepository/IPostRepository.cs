using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.PostRepository
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();

        Post GetPostById(int postId);

        Post CreatePost(Post post);

        Post UpdatePostContent(int postId, Post postContentDto);

        Post DeletePostById(int postId);
    }
}
