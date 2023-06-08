using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.ReposContracts
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAllComments();

        public Comment CreateComment(Comment comment);

        public bool UpdateComment(int commentId, Comment comment);

        public void DeleteComment(Comment comment);

        public Comment FindCommentById(int commentId);

        public IEnumerable<Comment> FindCommentsByPostId(int postId);
    }
}
