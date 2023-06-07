using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Helpers;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.CommentService
{
    public class CommentService
    {
        private readonly IForumSystemRepository repo;

        public CommentService(IForumSystemRepository repository)
        {
            repo = repository;
        }

        public CommentDTO GetCommentById(int commentId)
        {
            Comment comment = repo.FindCommentById(commentId);
            return CommentMapper.MapToDTO(comment);
        }

        public IEnumerable<CommentDTO> GetCommentsForPost(int postId)
        {
            IEnumerable<Comment> comments = repo.FindCommentsByPostId(postId);
            return CommentMapper.MapToDTOList(comments);
        }

        public void CreateComment(CommentDTO commentDTO)
        {
            Comment comment = CommentMapper.MapToModel(commentDTO);
            comment.CreatedOn = DateTime.Now;
            repo.CreateComment(comment);
        }

        public void UpdateComment(CommentDTO commentDTO)
        {
            Comment comment = repo.FindCommentById(commentDTO.Id);

            if (comment == null)
            {
                // Handle error or throw an exception
                return;
            }

            comment.Content = commentDTO.Content;

            repo.UpdateComment(commentDTO.Id, comment);
        }

        public void DeleteComment(int commentId)
        {
            Comment comment = repo.FindCommentById(commentId);

            if (comment == null)
            {
                // Handle error or throw an exception
                return;
            }

            comment.IsDeleted = true;
            comment.DeletedOn = DateTime.Now;

            repo.UpdateComment(commentId, comment);
        }
    }
}
