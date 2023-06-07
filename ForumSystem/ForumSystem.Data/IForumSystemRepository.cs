using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess;

public interface IForumSystemRepository
{
    public IEnumerable<Post> GetPosts();

    public Post CreatePost(Post post);

    public Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto);

    public Post DeletePostById(int postId);

    public Post FindPostById(int postId);

    //User

    public IEnumerable<User> GetAllUsers();

    public User CreateUser(User user);

    public bool UpdateUser(User user);

    public void DeleteUser(User user);

    public User FindUserById(int userId);

    // Comment

    public IEnumerable<Comment> GetAllComments();

    public Comment CreateComment(Comment comment);

    public bool UpdateComment(int commentId, Comment comment);

    public void DeleteComment(Comment comment);

    public Comment FindCommentById(int commentId);

    public IEnumerable<Comment> FindCommentsByPostId(int postId);
}
