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
        private readonly PostMapper postMapper;

        public PostController(IPostService postService, PostMapper postMapper)
        {
            this.postService = postService;
            this.postMapper = postMapper;
        }

        [HttpGet("")]
        public IActionResult GetPosts()
        {
            IList<Post> result = this.postService.GetAllPosts();
            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            try
            {
                Post post = this.postService.FindPostById(id);
                GetPostDto postDto = postMapper.MapGet(post);

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
    }
}