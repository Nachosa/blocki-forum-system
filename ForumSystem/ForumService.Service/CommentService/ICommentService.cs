using DTO.CommentDTO;
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
        public IList<CommentDTO> GetAllComments();

        public Comment CreateComment(CommentDTO commentDTO);

        public Comment UpdateCommentContent(int commentId, CommentDTO commentDTO);

        public void DeleteComment(Comment comment);

        public CommentDTO FindCommentById(int commentId);
    }
}
