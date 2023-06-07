using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Dtos;
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

        public static IList<Comment> comments = new List<Comment>()
        {
            new Comment()
            {
                Id = 1,
                PostId = 1,
                AuthorId = 1,
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
            var post = posts.FirstOrDefault(post => post.Id == postId);
            return post ?? throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
        }

        public Post UpdatePostContent(Post post, UpdatePostContentDto postContentDto)
        {
            post.Content = postContentDto.Content;
            return post;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public User CreateUser(User user)
        {
            user.Id = users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            users.Add(user);
            return user;
        }

        public bool UpdateUser(User user)
        {
            var userToupdate = users.FirstOrDefault(u => u.Id == user.Id);
            userToupdate.FirstName= user.FirstName;
            userToupdate.LastName= user.LastName;
            userToupdate.Email= user.Email;           
            return true;
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public User FindUserById(int Id)
        {
            var user = users.FirstOrDefault(u => u.Id == Id);
            return user;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return comments;
        }

        public Comment CreateComment(Comment comment)
        {
            comments.Add(comment);
            return comment;
        }

        public bool UpdateComment(int commentId, Comment comment)
        {
            var existingComment = FindCommentById(commentId);

            if (existingComment == null)
            {
                return false;
            }

            existingComment.Content = comment.Content;

            return true;
        }

        public void DeleteComment(Comment comment)
        {
            comments.Remove(comment);
        }

        public Comment FindCommentById(int commentId)
        {
            var comment = comments.FirstOrDefault(comment => comment.Id == commentId);
            return comment ?? throw new ArgumentNullException($"Comment with id={commentId} doesn't exist.");
        }
    }
}
