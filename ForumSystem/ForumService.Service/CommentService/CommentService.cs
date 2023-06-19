using AutoMapper;
using DTO.CommentDTO;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.DataAccess.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepo;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CommentService(ICommentRepository commentRepo, IMapper mapper, IUserRepository userRepository)
        {
            this.commentRepo = commentRepo;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public Comment CreateComment(CreateCommentDto commentDTO)
        {
            Comment comment = mapper.Map<Comment>(commentDTO);
            return commentRepo.CreateComment(comment);
        }

        public bool DeleteCommentById(int commentId)
        {
            var commentToDelete = commentRepo.FindCommentById(commentId) ?? throw new EntityNotFoundException($"Comment with ID = {commentId} was not found!");
            commentRepo.DeleteCommentById(commentId);
            return true;
        }

        public GetCommentDto FindCommentById(int commentId)
        {
            return mapper.Map<GetCommentDto>(commentRepo.FindCommentById(commentId)) ?? throw new EntityNotFoundException($"Comment with Id={commentId} was not found!");
        }

        public IList<GetCommentDto> GetAllComments(CommentQueryParameters queryParams)
        {
            IList<Comment> comments = commentRepo.GetAllComments().ToList();

            if (!comments.Any())
            {
                throw new EntityNotFoundException("There aren't any comments yet!");
            }

            return comments.Select(comment => mapper.Map<GetCommentDto>(comment)).ToList();
        }

        public Comment UpdateCommentContent(int commentId, UpdateCommentContentDto commentDTO)
        {
            var mappedComment = mapper.Map<Comment>(commentDTO);

            return commentRepo.UpdateComment(commentId, mappedComment);
        }
    }
}