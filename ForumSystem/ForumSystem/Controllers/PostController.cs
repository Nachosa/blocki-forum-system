using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.Business.Models;
using ForumSystem.Business;

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
        public IList<Post> GetPosts()
        {
            return postService.GetAllPosts();
        }
    }
}
