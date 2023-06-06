using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Models;

namespace ForumSystem.DataAccess
{
    public class ForumSystemRepository : IForumSystemRepository
    {
        public static IList<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Gosho",
                LastName = "Goshev",
                Username = "goshoXx123",
                Email = "gosho@gmail.com",
                Password = "1234567890",
                Role = Role.User
                
            }
        };
        public static IList<Post> posts = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                UserId = 1,
                Title = "Title",
                Content = "Content"
            }
        };

        public IEnumerable<Post> GetAllPosts()
        {
            return new List<Post>(posts);
        }

        public Post CreatePost(Post post)
        {
            posts.Add(post);
            return post;
        }

        public void DeletePost(Post post)
        {
            posts.Remove(post);
        }

        public Post FindPostById(int postId)
        {
            return posts.FirstOrDefault(post => post.Id == postId);
        }

        public bool UpdatePost(int postId, Post post)
        {
            var existingPost = FindPostById(postId);

            if (existingPost == null)
            {
                return false;
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.Dislikes = post.Dislikes;
            existingPost.Likes = post.Likes;

            return true;
        }
    }
}
