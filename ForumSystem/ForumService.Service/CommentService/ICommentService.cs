using DTO.CommentDTO;
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
        public IList<GetCommentDto> GetAllComments(CommentQueryParameters queryParams);

        public Comment CreateComment(CreateCommentDto commentDTO, int postId);

        public Comment UpdateCommentContent(int commentId, UpdateCommentContentDto commentDTO, string username);

        //public bool DeleteComment(Comment comment, string userName, int? commentId);

        public GetCommentDto FindCommentById(int commentId);

        public bool DeleteCommentById(int commentId, string username);
    }
}
