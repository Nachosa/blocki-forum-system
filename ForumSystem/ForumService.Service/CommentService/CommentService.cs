using AutoMapper;
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
    public class CommentService : ICommentService
    {
        private readonly IForumSystemRepository repo;
        private readonly IMapper mapper;

        public CommentService(IForumSystemRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        public Comment CreateComment(CommentDTO commentDTO)
        {
            Comment comment = mapper.Map<Comment>(commentDTO);

            comment.Id = Comment.Count;
            Comment.Count += 1;

            repo.CreateComment(comment);
            return comment;
        }

        public void DeleteComment(Comment comment)
        {
            repo.DeleteComment(comment);
        }

        public CommentDTO FindCommentById(int commentId)
        {
            return mapper.Map<CommentDTO>(repo.FindCommentById(commentId));
        }

        public IList<CommentDTO> GetAllComments()
        {
            IList<Comment> comments = repo.GetAllComments().ToList();
            return comments.Select(comment => mapper.Map<CommentDTO>(comment)).ToList();
        }

        // update comment in repo should take a commentDTO
        public Comment UpdateCommentContent(int commentId, CommentDTO commentDTO)
        {
            return repo.UpdateComment(commentId, commentDTO);
        }
    }
}