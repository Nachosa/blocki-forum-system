using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using DTO.CommentDTO;
using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.QueryParams;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentApiController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentApiController(ICommentService commentService)
        {
            this.commentService = commentService;
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
                Comment comment = commentService.DeleteCommentById(id);
                return StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}
