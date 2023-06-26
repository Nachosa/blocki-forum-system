using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.CommentRepo
{
    public interface ICommentRepository
    {
        public bool DeleteCommentById(int commentId);

        public Comment CreateComment(Comment comment);

        public Comment GetCommentById(int commentId);

        public Comment UpdateCommentContent(Comment comment, int commentId);

        public IEnumerable<Comment> GetComments(CommentQueryParameters queryParameters);

        public IEnumerable<Comment> GetCommentsByPostId(int postId);
    }
}