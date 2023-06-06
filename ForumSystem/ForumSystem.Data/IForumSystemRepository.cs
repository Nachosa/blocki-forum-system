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

    public bool UpdatePost(int postId, Post post);

    public void DeletePost(Post post);

    public Post FindPostById(int postId);

    //User

    public IEnumerable<User> GetAllUsers();

    public User CreateUser(User user);

    public bool UpdateUser(int userId, User user);

    public void DeleteUser(User user);

    public User FindUserById(int userId);
}
