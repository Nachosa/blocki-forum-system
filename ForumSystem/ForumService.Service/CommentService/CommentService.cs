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
        private readonly ICommentRepository commentRepo;
        private readonly IMapper mapper;
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;

        public CommentService(ICommentRepository commentRepo, IMapper mapper, IUserRepository userRepository, IPostService postService, IUserService userService)
        {
            this.commentRepo = commentRepo;
            this.postService = postService;
            this.mapper = mapper;
            this.userService = userService;
            this.userRepository = userRepository;
        }

        public Comment CreateComment(CreateCommentDto commentDTO, int postId)
        {
            Comment comment = mapper.Map<Comment>(commentDTO);
            Post post = postService.GetPostById(postId);
            post.Comments.Add(comment);
            return commentRepo.CreateComment(comment);
        }

        public bool DeleteCommentById(int commentId, string username)
        {
            var comment = FindCommentById(commentId);
            var user = userService.GetUserByUserName(username);

            if (comment.UserId != user.Id && user.Role.Id != 3) // user is neither admin or author
            {
                throw new ArgumentException("You have to be the author of this comment or an admin in order to delete it!");
            }

            // move the exception to the repo
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

        public Comment UpdateCommentContent(int commentId, UpdateCommentContentDto commentDTO, string username)
        {
            var mappedComment = mapper.Map<Comment>(commentDTO);

            var comment = FindCommentById(commentId);
            var user = userService.GetUserByUserName(username);

            if (comment.UserId != user.Id)
            {
                throw new ArgumentException("You have to be the author of this comment in order to update it!");
            }

            return commentRepo.UpdateComment(commentId, mappedComment);
        }
    }
}