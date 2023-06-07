using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.CommentService
{
    public interface ICommentService
    {
        public IList<Comment> GetAllComments();

        public Comment CreateComment(CommentDTO commentDTO);

        public Comment UpdateCommentContent(Comment comment, CommentDTO commentDTO);

        public void DeleteComment(Comment comment);

        public Comment FindCommentById(int commentId);
    }
}
