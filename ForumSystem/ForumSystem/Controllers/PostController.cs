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

        public PostController(IPostService postService) 
        {
            this.postService = postService;
            this.postMapper = new PostMapper();
        } 

        [HttpGet("")]
        public IList<Post> GetPosts()
        {
            return postService.GetAllPosts();
        }

        [HttpPost("")]
        public Post CreatePost(CreatePostDto postDto)
        {
            Post post = postMapper.Map(postDto);
            postService.CreatePost(post);
            return post;
        }

        [HttpPut("")]
    }
}
