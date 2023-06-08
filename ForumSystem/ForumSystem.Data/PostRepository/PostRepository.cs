using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;

namespace ForumSystem.DataAccess.PostRepository
{
    public class PostRepository : IPostRepository
    {
        public static IList<Post> posts = new List<Post>();

        public IEnumerable<Post> GetPosts()
        {
            return new List<Post>(posts);
        }

        public Post CreatePost(Post post)
        {
            posts.Add(post);
            return post;
        }

        public bool DeletePostById(int postId)
        {
            var post = posts.FirstOrDefault(post => post.Id == postId);
            if (post == null)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                posts.Remove(post);
            return true;
        }

        public Post GetPostById(int postId)
        {
            var post = posts.FirstOrDefault(post => post.Id == postId);
            return post ?? throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
        }

        public Post UpdatePostContent(int postId, Post newPost)
        {
            var post = posts.FirstOrDefault(post => post.Id == postId);
            if (post == null)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                post.Content = newPost.Content;
            return post;
        }
    }
}
