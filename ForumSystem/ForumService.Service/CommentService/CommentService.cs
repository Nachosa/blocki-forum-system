using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
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
        private readonly IAuthManager authManager;
        private readonly ICommentRepository commentRepository;
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;

        public CommentService(IAuthManager authManager, ICommentRepository commentRepository, IPostService postService, IUserService userService, IUserRepository userRepository)
        {
            this.authManager = authManager;
            this.commentRepository = commentRepository;
            this.postService = postService;
            this.userService = userService;
            this.userRepository = userRepository;
        }

        public bool DeleteCommentById(int commentId, string username)
        {
            var comment = GetCommentById(commentId);
            var user = userService.GetUserByUserName(username);

            if (!authManager.AdminCheck(user) && comment.UserId != user.Id)
            {
                throw new UnauthorizedOperationException("Only the comment's author can delete the comment.");
            }

            if (authManager.BlockedCheck(user))
            {
				throw new UnauthorizedOperationException("You are blocked and cannot perform this action.");
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

        public Comment UpdateComment(Comment comment, int commentId)
        {
            return commentRepository.UpdateCommentContent(comment, commentId);
        }

        public Comment UpdateCommentContent(Comment comment, int commentId, string username)
        {
            var user = userService.GetUserByUserName(username);

			if (!authManager.AdminCheck(user) && comment.UserId != user.Id)
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

		public bool LikeComment(int commentId, string userName)
        {
			var comment = commentRepository.GetCommentById(commentId);
			var user = userRepository.GetUserByUserName(userName);

			var like = commentRepository.GetLike(commentId, user.Id);

			if (like == null)
			{
				commentRepository.CreateLike(comment, user);
			}
			else if (like != null & (like.IsDeleted || like.IsDislike))
			{
				commentRepository.LikeComment(like);
			}
			else
			{
				commentRepository.DeleteLike(like);
			}

			return true;
		}

		public bool DislikeComment(int commentId, string userName)
        {
			var comment = commentRepository.GetCommentById(commentId);
			var user = userRepository.GetUserByUserName(userName);

			var like = commentRepository.GetLike(commentId, user.Id);

			if (like == null)
			{
				commentRepository.CreateLike(comment, user);
			}
			else if (like != null & (like.IsDeleted || !like.IsDislike))
			{
				commentRepository.DislikeComment(like);
			}
			else
			{
				commentRepository.DeleteLike(like);
			}

			return true;
		}
	}
}