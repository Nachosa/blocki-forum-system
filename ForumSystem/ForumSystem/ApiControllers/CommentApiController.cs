using AutoMapper;
using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystemDTO.CommentDTO;
using ForumSystemDTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ForumSystem.Web.ApiControllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentApiController : ControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public CommentApiController(IAuthManager authManager, ICommentService commentService, IMapper mapper)
        {
            this.authManager = authManager;
            this.commentService = commentService;
            this.mapper = mapper;
        }

        [HttpPost("{postId}")]
        public IActionResult CreateComment([FromBody] CreateCommentDto createCommentDto, [FromHeader] string credentials, int postId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                var comment = mapper.Map<Comment>(createCommentDto);

                return StatusCode(StatusCodes.Status200OK, commentService.CreateComment(comment, postId));
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteCommentById([FromHeader] string credentials, int commentId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string username = credentials.Split(':')[0];

                return StatusCode(StatusCodes.Status200OK, commentService.DeleteCommentById(commentId, username));
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
        }

        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                var comment = commentService.GetCommentById(commentId);

                return StatusCode(StatusCodes.Status200OK, mapper.Map<GetCommentDto>(comment));
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("")]
        public IActionResult GetComments([FromQuery] CommentQueryParameters queryParameters)
        {
            try
            {
                if (queryParameters.MaxDate < queryParameters.MinDate)
                {
                    throw new ArgumentException("Invalid date range.");
                }

                var comments = commentService.GetComments(queryParameters);

                return StatusCode(StatusCodes.Status200OK, comments.Select(comment => mapper.Map<GetCommentDto>(comment)));
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPatch("{commentId}")]
        public IActionResult UpdateCommentContent([FromBody] UpdateCommentContentDto commentContentDto, [FromHeader] string credentials, int commentId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string username = credentials.Split(':')[0];
                var comment = mapper.Map<Comment>(commentContentDto);

                return StatusCode(StatusCodes.Status200OK, commentService.UpdateCommentContent(comment, commentId, username));
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
        }
    }
}