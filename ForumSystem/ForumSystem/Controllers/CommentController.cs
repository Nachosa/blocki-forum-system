using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using DTO.CommentDTO;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet("")]
        public IActionResult GetComments()
        {
            return StatusCode(StatusCodes.Status200OK, commentService.GetAllComments());
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
