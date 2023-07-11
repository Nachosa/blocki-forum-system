using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.CommentService
{
    public interface ICommentService
    {
        public bool DeleteCommentById(int commentId, string username);

        public Comment CreateComment(Comment comment, int postId);

        public Comment GetCommentById(int commentId);

        public Comment UpdateComment(Comment comment, int commentId);

        public Comment UpdateCommentContent(Comment comment, int commentId, string username);

        public ICollection<Comment> GetComments(CommentQueryParameters queryParameters);

		bool LikeComment(int commentId, string userName);

		bool DislikeComment(int commentId, string userName);
	}
}