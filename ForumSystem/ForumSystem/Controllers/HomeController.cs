using ForumSystem.Business.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.Business.Models;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly IPostService postService;

        public HomeController(IPostService postService) 
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
