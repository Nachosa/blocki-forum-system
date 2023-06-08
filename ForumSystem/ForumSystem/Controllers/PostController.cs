using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.Models;
using ForumSystem.Business;
using ForumSystem.DataAccess.Helpers;
using ForumSystem.DataAccess.Dtos;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("")]
        public IActionResult GetPosts()
        {
            return this.StatusCode(StatusCodes.Status200OK, this.postService.GetPosts());
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            try
            {
                GetPostDto postDto = this.postService.GetPostById(id);
                return this.StatusCode(StatusCodes.Status200OK, postDto);
            }
            catch (ArgumentNullException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreatePost([FromBody] CreatePostDto postDto)
        {
            postService.CreatePost(postDto);
            return this.StatusCode(StatusCodes.Status200OK, postDto);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePostContent(int id, [FromBody] UpdatePostContentDto postContentDto)
        {
            try
            {
                Post post = postService.UpdatePostContent(id, postContentDto);
                return this.StatusCode(StatusCodes.Status200OK, post);
            }
            catch (ArgumentNullException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePostById(int id)
        {
            try
            {
                Post post = this.postService.DeletePostById(id);
                return this.StatusCode(StatusCodes.Status200OK, post);
            }
            catch (ArgumentNullException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}