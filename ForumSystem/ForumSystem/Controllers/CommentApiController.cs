using AutoMapper;
using DTO.CommentDTO;
using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystemDTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentApiController : ControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly ICommentService commentService;

        public CommentApiController(IAuthManager authManager, ICommentService commentService)
        {
            this.authManager = authManager;
            this.commentService = commentService;
        }

        [HttpPost("{postId}")]
        public IActionResult CreateComment([FromBody] CreateCommentDto commentDto, [FromHeader] string credentials, int postId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                return StatusCode(StatusCodes.Status200OK, commentService.CreateComment(commentDto, postId));
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

        [HttpDelete("{commentId}")]
        public IActionResult DeleteCommentById([FromHeader] string credentials, int commentId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string username = credentials.Split(':')[0];

                return StatusCode(StatusCodes.Status200OK, commentService.DeleteCommentById(commentId, username));
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
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

        [HttpGet("")]
        public IActionResult GetComments([FromQuery] CommentQueryParameters queryParameters)
        {
            try
            {
                if (queryParameters.MaxDate < queryParameters.MinDate)
                {
                    throw new ArgumentException("Invalid date range!");
                }

                return StatusCode(StatusCodes.Status200OK, commentService.GetAllComments(queryParameters));
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPatch("{commentId}")]
        public IActionResult UpdateCommentContent([FromBody] UpdateCommentContentDto commentContentDto, [FromHeader] string credentials, int commentId)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string username = credentials.Split(':')[0];

                return StatusCode(StatusCodes.Status200OK, commentService.UpdateCommentContent(commentId, username, commentContentDto));
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}