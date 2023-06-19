using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using DTO.CommentDTO;
using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.QueryParams;
using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystemDTO.UserDTO;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentApiController : ControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly ICommentService commentService;
        private readonly IPostService postService;
        private readonly IUserService userService;

        public CommentApiController(IAuthManager authManager, ICommentService commentService, IPostService postService, IUserService userService)
        {
            this.authManager = authManager;
            this.commentService = commentService;
            this.postService = postService;
            this.userService = userService;
        }

        [HttpGet("")]
        public IActionResult GetComments([FromQuery] CommentQueryParameters queryParams)
        {
            try
            {
                if (queryParams.MaxDate < queryParams.MinDate)
                {
                    throw new ArgumentException("Invalid date range!");
                }

                return StatusCode(StatusCodes.Status200OK, commentService.GetAllComments(queryParams));
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                GetCommentDto commentDTO = commentService.FindCommentById(commentId);
                return StatusCode(StatusCodes.Status200OK, commentDTO);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        //[HttpPost("")]
        //public IActionResult CreateComment([FromHeader] string credentials, [FromBody] CreateCommentDto commentDto)
        //{
        //    authManager.BlockedCheck(credentials);
        //    commentService.CreateComment(commentDto);
        //    return StatusCode(StatusCodes.Status200OK, commentDto);
        //}

        [HttpPost("{postId}")]
        public IActionResult CreateComment([FromBody] CreateCommentDto commentDto, [FromHeader] string credentials, int postId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                Comment comment = commentService.CreateComment(commentDto);
                Post post = postService.GetPostById(postId);
                post.Comments.Add(comment);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPatch("{commentId}")]
        public IActionResult UpdateCommentContent([FromBody] UpdateCommentContentDto commentContentDto, [FromHeader] string credentials, [FromQuery] string username, int commentId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                var comment = commentService.FindCommentById(commentId);
                var user = userService.GetUserByUserName(username);

                if (comment.UserId != user.Id)
                {
                    throw new ArgumentException("You have to be the author of this comment in order to update it!");
                }

                return StatusCode(StatusCodes.Status200OK, commentService.UpdateCommentContent(commentId, commentContentDto));
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteCommentById([FromHeader] string credentials, [FromQuery] string username, int commentId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                var comment = commentService.FindCommentById(commentId);
                var user = userService.GetUserByUserName(username);

                if (comment.UserId != user.Id && user.Role.Id != 1) // user is neither admin or author
                {
                    throw new ArgumentException("You have to be the author of this comment or an admin in order to delete it!");
                }

                var isDeleted = commentService.DeleteCommentById(commentId);

                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}
