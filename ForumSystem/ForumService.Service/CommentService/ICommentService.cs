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

        public Comment CreateComment(CreateCommentDto commentDTO);

        public Comment UpdateCommentContent(int commentId, UpdateCommentContentDto commentDTO);

        public void DeleteComment(Comment comment);

        public GetCommentDto FindCommentById(int commentId);

        public Comment DeleteCommentById(int commentId);
    }
}
