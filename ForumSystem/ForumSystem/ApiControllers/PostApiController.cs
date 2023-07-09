using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.Models;
using ForumSystem.Business;
using ForumSystemDTO.PostDTO;
using ForumSystem.Api.QueryParams;
using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
using Microsoft.Extensions.Hosting;
using ForumSystem.DataAccess.Exceptions;
using ForumSystemDTO.TagDTO;

namespace ForumSystem.Web.ApiControllers
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
            try
            {
                //Най-вероятно ще трябват още проверки.
                if (queryParams.MaxDate < queryParams.MinDate)
                {
                    throw new ArgumentException("Invalid date range!");
                }
                authManager.UserCheck(credentials);

                var posts = postService.GetPosts(queryParams);
                var mappedPosts = posts.Select(post => postMapper.Map<GetPostDtoAbbreviated>(post)).ToList();
                return StatusCode(StatusCodes.Status200OK, mappedPosts);
            }
            //Трябва да си оправя exception-ите навсякъде, може би да са къстъм?
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message); ;
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message); ;
            }
        }

        //Може би трябва да се премести в админ частта.
        [HttpGet("{id}")]
        public IActionResult GetPostById(int id, [FromHeader] string credentials)
        {
            try
            {
                authManager.AdminCheck(credentials);
                var post = postService.GetPostById(id);
                var mappedPost = postMapper.Map<GetPostDto>(post);
                return StatusCode(StatusCodes.Status200OK, mappedPost);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message); ;
            }
        }

        [HttpPost("")]
        public IActionResult CreatePost([FromHeader] string credentials, [FromBody] CreatePostDto postDto)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string[] usernameAndPassword = credentials.Split(':');
                string userName = usernameAndPassword[0];

                var post = postMapper.Map<Post>(postDto);
                postService.CreatePost(post, userName);
                return StatusCode(StatusCodes.Status200OK, postDto);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPost("{postId}/like")]
        public IActionResult LikePost(int postId, [FromHeader] string credentials)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string[] usernameAndPassword = credentials.Split(':');
                string userName = usernameAndPassword[0];

                postService.LikePost(postId, userName);
                return StatusCode(StatusCodes.Status200OK, true);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            //catch (DuplicateEntityException e)
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            //}
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

		[HttpPost("{postId}/dislike")]
		public IActionResult DislikePost(int postId, [FromHeader] string credentials)
		{
			try
			{
				authManager.BlockedCheck(credentials);
				string[] usernameAndPassword = credentials.Split(':');
				string userName = usernameAndPassword[0];

				postService.DislikePost(postId, userName);
				return StatusCode(StatusCodes.Status200OK, true);
			}
			catch (UnauthenticatedOperationException e)
			{
				return StatusCode(StatusCodes.Status400BadRequest, e.Message);
			}
			//catch (DuplicateEntityException e)
			//{
			//    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
			//}
			catch (EntityNotFoundException e)
			{
				return StatusCode(StatusCodes.Status400BadRequest, e.Message);
			}
		}

		[HttpPatch("{postId}/tag")]
        public IActionResult TagPost(int postId, [FromHeader] string credentials, [FromBody] TagDto tagDto)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string[] usernameAndPassword = credentials.Split(':');
                string userName = usernameAndPassword[0];

                //postMapper не е добро име
                var tag = postMapper.Map<Tag>(tagDto);
                postService.TagPost(postId, userName, tag);
                return StatusCode(StatusCodes.Status200OK, true);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePostContent(int id, [FromBody] UpdatePostContentDto postContentDto, [FromHeader] string credentials)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string[] usernameAndPassword = credentials.Split(':');
                string userName = usernameAndPassword[0];

                var mappedPost = postMapper.Map<Post>(postContentDto);
                var updatedPost = postService.UpdatePostContent(id, mappedPost, userName);
                var updatedPostDto = postMapper.Map<GetPostDto>(updatedPost);
                return StatusCode(StatusCodes.Status200OK, updatedPostDto);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePostById(int id, [FromHeader] string credentials)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string[] usernameAndPassword = credentials.Split(':');
                string userName = usernameAndPassword[0];

                var isDeleted = postService.DeletePostById(id, userName);
                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}