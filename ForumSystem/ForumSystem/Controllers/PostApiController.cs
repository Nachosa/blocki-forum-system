using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.Models;
using ForumSystem.Business;
using ForumSystemDTO.PostDTO;
using ForumSystem.Api.QueryParams;
using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
using Microsoft.Extensions.Hosting;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostApiController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IAuthManager authManager;
        private readonly IMapper postMapper;

        public PostApiController(IPostService postService, IAuthManager authManager, IMapper postMapper)
        {
            this.authManager = authManager;
            this.postService = postService;
            this.postMapper = postMapper;
        }

        [HttpGet("")]
        public IActionResult GetPosts([FromHeader] string credentials, [FromQuery] PostQueryParameters queryParams)
        {
            authManager.UserCheck(credentials);
            try
            {
                //Най-вероятно ще трябват още проверки.
                if (queryParams.MaxDate < queryParams.MinDate)
                {
                    throw new ArgumentException("Invalid date range!");
                }
                var posts = postService.GetPosts(queryParams);
                var mappedPosts = posts.Select(post => postMapper.Map<GetPostDto>(post)).ToList();
                return this.StatusCode(StatusCodes.Status200OK, mappedPosts);
            }
            //Трябва да си оправя exception-ите навсякъде, може би да са къстъм?
            catch (ArgumentException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message); ;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id, [FromHeader] string credentials)
        {
            authManager.AdminCheck(credentials);
            try
            {
                var post = this.postService.GetPostById(id);
                var mappedPost = postMapper.Map<GetPostDto>(post);
                return this.StatusCode(StatusCodes.Status200OK, mappedPost);
            }
            catch (ArgumentNullException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreatePost([FromHeader] string credentials, [FromBody] CreatePostDto postDto)
        {
            authManager.BlockedCheck(credentials);
            var post = postMapper.Map<Post>(postDto);
            postService.CreatePost(post);
            return this.StatusCode(StatusCodes.Status200OK, postDto);
        }

        //В момента прави безкраен цикъл и не работи!
        [HttpPatch("{id}")]
        public IActionResult UpdatePostContent(int id, [FromBody] UpdatePostContentDto postContentDto, [FromHeader] string credentials)
        {
            authManager.BlockedCheck(credentials);
            string[] usernameAndPassword = credentials.Split(':');
            string userName = usernameAndPassword[0];
            try
            {
                var mappedPost = postMapper.Map<Post>(postContentDto);
                mappedPost = postService.UpdatePostContent(id, mappedPost, userName);
                return this.StatusCode(StatusCodes.Status200OK, mappedPost);
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
                var isDeleted = this.postService.DeletePostById(id);
                return this.StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (ArgumentNullException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}