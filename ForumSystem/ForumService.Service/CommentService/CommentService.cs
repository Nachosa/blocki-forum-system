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
        private readonly IPostService postService;
        private readonly IUserService userService;

        public CommentService(ICommentRepository commentRepository, IPostService postService, IUserService userService)
        {
            this.commentRepository = commentRepository;
            this.postService = postService;
            this.userService = userService;
        }

        public bool DeleteCommentById(int commentId, string username)
        {
            var comment = GetCommentById(commentId);
            var user = userService.GetUserByUserName(username);

            if (comment.UserId != user.Id)
            {
                throw new UnauthorizedOperationException("Only the comment's author can delete the comment.");
            }

            return commentRepository.DeleteCommentById(commentId);
        }

        public Comment CreateComment(Comment comment, int postId)
        {
            var post = postService.GetPostById(postId);

            post.Comments.Add(comment);
            return commentRepository.CreateComment(comment);
        }

        public Comment GetCommentById(int commentId)
        {
            var comment = commentRepository.GetCommentById(commentId);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }

            return comment;
        }

        public Comment UpdateCommentContent(Comment comment, int commentId, string username)
        {
            var user = userService.GetUserByUserName(username);

            if (comment.UserId != user.Id)
            {
                throw new UnauthorizedOperationException("Only the comment's author can update the comment.");
            }

            return commentRepository.UpdateCommentContent(comment, commentId);
        }

        public ICollection<Comment> GetComments(CommentQueryParameters queryParameters)
        {
            var comments = commentRepository.GetComments(queryParameters).ToList();

            if (!comments.Any())
            {
                throw new EntityNotFoundException("No comments found. The collection is currently empty.");
            }

            return comments;
        }
    }
}