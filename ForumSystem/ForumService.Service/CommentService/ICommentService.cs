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
        public bool DeleteCommentById(int commentId, string username);

        public Comment CreateComment(CreateCommentDto commentDTO, int postId);

        public Comment UpdateCommentContent(int commentId, string username, UpdateCommentContentDto commentDTO);

        public GetCommentDto FindCommentById(int commentId);

        public IList<GetCommentDto> GetAllComments(CommentQueryParameters queryParams);
    }
}