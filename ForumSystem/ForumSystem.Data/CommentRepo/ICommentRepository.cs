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
        public bool DeleteComment(Comment comment);

        public bool DeleteCommentById(int commentId);

        public Comment CreateComment(Comment comment);

        public Comment FindCommentById(int commentId);

        public Comment UpdateComment(Comment comment, int commentId);

        public IEnumerable<Comment> FindCommentsByPostId(int postId);

        public IEnumerable<Comment> GetAllComments();
    }
}