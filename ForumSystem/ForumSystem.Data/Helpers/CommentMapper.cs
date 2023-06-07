using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Helpers
{
    public static class CommentMapper
    {
        public static CommentDTO MapToDTO(Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                AuthorId = comment.AuthorId,
                PostId = comment.PostId,
                IsDeleted = comment.IsDeleted,
                DeletedOn = comment.DeletedOn,
                Likes = comment.Likes,
                Dislikes = comment.Dislikes
            };
        }

        public static Comment MapToModel(CommentDTO commentDTO)
        {
            return new Comment
            {
                Id = commentDTO.Id,
                Content = commentDTO.Content,
                AuthorId = commentDTO.AuthorId,
                PostId = commentDTO.PostId,
                IsDeleted = commentDTO.IsDeleted,
                DeletedOn = commentDTO.DeletedOn,
                Likes = commentDTO.Likes,
                Dislikes = commentDTO.Dislikes
            };
        }

        public static List<CommentDTO> MapToDTOList(IEnumerable<Comment> comments)
        {
            List<CommentDTO> commentDTOs = new List<CommentDTO>();

            foreach (Comment comment in comments)
            {
                commentDTOs.Add(MapToDTO(comment));
            }

            return commentDTOs;
        }
    }
}
