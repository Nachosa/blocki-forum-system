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
            },
            new User()
            {
                Id = 2,
                FirstName = "Nikolai",
                LastName = "Barekov",
                Username = "BarekaXx123",
                Email = "Nikolai@gmail.com",
                Password = "1234567890",
                Role = Role.User

            },
            new User()
            {
                Id = 3,
                FirstName = "Boiko",
                LastName = "Borisov",
                Username = "BokoMoko",
                Email = "gosho@gmail.com",
                Password = "1234567890",
                Role = Role.User

            },
            new User()
            {
                Id = 4,
                FirstName = "Cvetan",
                LastName = "Cvetanov",
                Username = "Cvete123",
                Email = "Cvetan@gmail.com",
                Password = "1234567890",
                Role = Role.User

            },
            new User()
            {
                Id = 5,
                FirstName = "Kosta",
                LastName = "Kopeikin",
                Username = "BrainDamage123",
                Email = "Kopeikin@gmail.com",
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

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public User CreateUser(User user)
        {
            users.Add(user);
            return user;
        }

        public bool UpdateUser(int userId, User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public User FindUserById(int Id)
        {
            var user = users.FirstOrDefault(u => u.Id == Id);
            return user ?? throw new Exception($"User with Id={Id} was not found!");
        }
    }
}
