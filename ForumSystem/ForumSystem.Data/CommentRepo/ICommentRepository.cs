using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.CommentRepo
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAllComments();

        public Comment CreateComment(Comment comment);

        public Comment UpdateComment(int commentId, Comment comment);

        public void DeleteComment(Comment comment);

        public Comment FindCommentById(int commentId);

        public IEnumerable<Comment> FindCommentsByPostId(int postId);

        public Comment DeleteCommentById(int commentId);
    }
}
