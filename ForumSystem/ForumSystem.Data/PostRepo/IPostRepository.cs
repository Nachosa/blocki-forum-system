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

        //Tag GetTagWithName(string name);

        //ICollection<Post> GetPostsWithTag(string tag1);

        Post GetPostById(int postId);

        int GetPostsCount();

        ICollection<Post> GetUserPosts(int userId, PostQueryParameters queryParameters);

        Post CreatePost(Post post);

        public Like GetLike(int postId, int userId);

		bool CreateLike(Post post, User user);

		bool LikePost(Like like);

		bool DislikePost(Like like);

        public bool DeleteLike(Like like);

        bool TagPost(Post post, Tag tag);

        Post UpdatePostContent(Post newPost, Post currPost);

        bool DeletePostById(int postId);
    }
}
