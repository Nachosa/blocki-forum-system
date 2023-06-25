using AutoMapper;
using DTO.CommentDTO;
using ForumSystem.Business.UserService;
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
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        private readonly IPostService postService;
        private readonly IUserService userService;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IPostService postService, IUserService userService)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.postService = postService;
            this.userService = userService;
        }

        public bool DeleteCommentById(int commentId, string username)
        {
            var comment = FindCommentById(commentId);
            var user = userService.GetUserByUserName(username);

            if (comment.UserId != user.Id && user.Role.Id != 3)
            {
                throw new ArgumentException("You have to be the author of this comment or an admin in order to delete it!");
            }

            commentRepository.DeleteCommentById(commentId);
            return true;
        }

        public Comment CreateComment(CreateCommentDto commentDTO, int postId)
        {
            Comment comment = mapper.Map<Comment>(commentDTO);
            Post post = postService.GetPostById(postId);
            post.Comments.Add(comment);
            return commentRepository.CreateComment(comment);
        }

        public Comment UpdateCommentContent(int commentId, string username, UpdateCommentContentDto commentDTO)
        {
            var mappedComment = mapper.Map<Comment>(commentDTO);
            var user = userService.GetUserByUserName(username);

            if (mappedComment.UserId != user.Id)
            {
                throw new ArgumentException("You have to be the author of this comment in order to update it!");
            }

            return commentRepository.UpdateComment(mappedComment, commentId);
        }

        public GetCommentDto FindCommentById(int commentId)
        {
            return mapper.Map<GetCommentDto>(commentRepository.FindCommentById(commentId)) ?? throw new EntityNotFoundException($"Comment with Id = {commentId} was not found!");
        }

        public IList<GetCommentDto> GetAllComments(CommentQueryParameters queryParams)
        {
            IList<Comment> comments = commentRepository.GetAllComments().ToList();

            if (!comments.Any())
            {
                throw new EntityNotFoundException("There aren't any comments yet!");
            }

            return comments.Select(comment => mapper.Map<GetCommentDto>(comment)).ToList();
        }
    }
}