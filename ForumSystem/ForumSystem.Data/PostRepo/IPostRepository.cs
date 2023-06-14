using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.PostRepo
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts(PostQueryParameters queryParameters);

        Post GetPostById(int postId);

        ICollection<Post> GetUserPosts(int userId, PostQueryParameters queryParameters);

        Post CreatePost(Post post);

        Post UpdatePostContent(int postId, Post post, string userName);

        bool DeletePostById(int postId);
    }
}
