using AutoMapper;
using ForumSystem.Business;
using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
	public class PostController : Controller
	{
		private readonly IPostService postService;
		private readonly IMapper mapper;

		public PostController(IPostService postService, IMapper mapper)
		{
			this.postService = postService;
			this.mapper = mapper;
		}

		[HttpGet]
		public IActionResult Create() 
		{
			var createPostForm = new CreatePostViewModel();
			return View(createPostForm);
		}

		[HttpPost]
		public IActionResult Create(CreatePostViewModel createPostFormFilled)
		{
			try
			{
				if (!this.ModelState.IsValid)
				{
					return View(createPostFormFilled);
				}
				var pendingPost = mapper.Map<Post>(createPostFormFilled);
				//var pendingPost = new Post();
				//pendingPost.Title = createPostFormFilled.Title;
				//pendingPost.Content = createPostFormFilled.Content;

				//TODO: Refactor after Authentication.
				var newPost = postService.CreatePost(pendingPost, "Admin");

				return RedirectToAction("Details", "Home", new { id = newPost.Id }); ;
			}
			catch (Exception e)
			{
				//TODO: More precise exception handling.
				this.Response.StatusCode = StatusCodes.Status400BadRequest;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}
	}
}
