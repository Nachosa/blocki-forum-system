using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;

namespace ForumSystem.DataAccess.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
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

        public IEnumerable<Comment> GetAllComments()
        {
            return comments;
        }

        public Comment CreateComment(Comment comment)
        {
            comment.Id = comments.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            comments.Add(comment);
            return comment;
        }

        public Comment UpdateComment(int commentId, Comment comment)
        {
            var existingComment = FindCommentById(commentId);

            if (existingComment == null)
            {
                return comment;
            }

            existingComment.Content = comment.Content;

            return comment;
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

        public IEnumerable<Comment> FindCommentsByPostId(int postId)
        {
            var post = PostRepository.posts.FirstOrDefault(post => post.Id == postId);
            return post.Comments ?? throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
        }

        public Comment DeleteCommentById(int commentId)
        {
            var comment = comments.FirstOrDefault(comment => comment.Id == commentId);
            if (comment == null)
                throw new ArgumentNullException($"Comment with id={commentId} doesn't exist.");
            else
                comments.Remove(comment);
            return comment;
        }
    }
}
