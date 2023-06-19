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
        private readonly ICommentService commentService;
        private readonly IPostService postService;
        private readonly IAuthManager authManager;

        public CommentApiController(ICommentService commentService, IPostService postService, IAuthManager authManager)
        {
            this.commentService = commentService;
            this.postService = postService;
            this.authManager = authManager;
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

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                GetCommentDto commentDTO = commentService.FindCommentById(id);
                return StatusCode(StatusCodes.Status200OK, commentDTO);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateComment([FromBody] CreateCommentDto commentDto)
        {
            commentService.CreateComment(commentDto);
            return StatusCode(StatusCodes.Status200OK, commentDto);
        }

        // just testing
        [HttpPost("{postId}")]
        public IActionResult AddCommentToThread([FromHeader] string credentials, int postId,  [FromBody] CreateCommentDto commentDto)
        {
            try
            {
                authManager.UserCheck(credentials);

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

        [HttpPatch("{id}")]
        public IActionResult UpdateCommentContent(int id, [FromBody] UpdateCommentContentDto commentContentDto)
        {
            try
            {
                Comment comment = commentService.UpdateCommentContent(id, commentContentDto);
                return StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCommentById(int id)
        {
            try
            {
                var isDeleted = commentService.DeleteCommentById(id);
                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}
