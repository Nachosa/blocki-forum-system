using AutoMapper;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.CommentViewModels;
using ForumSystemDTO.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
	public class PostController : Controller
	{
		private readonly IPostService postService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
		private readonly IAuthManager authManager;

		public PostController(IPostService postService, IMapper mapper, IUserService userService, IAuthManager authManager)
		{
			this.postService = postService;
			this.mapper = mapper;
			this.userService = userService;
			this.authManager = authManager;
		}

		[HttpGet]
		public IActionResult PostDetails(int id)
		{
			try
			{
				var post = postService.GetPostById(id);
				var user = userService.GetUserById(post.UserId);

				var comments = post.Comments.Select(c => new CommentViewModel
				{
					CommentContent = c.Content,
					UserName = c.User?.Username ?? "Anonymous" // provide a fallback value if the User is null
				}).ToList();

				//TODO: Да се направи с мапър.
				var model = new PostDetailsViewModel
				{
					PostId = post.Id,
					Title = post.Title,
					CreatedBy = post.User.Username,
					CreatedOn = post.CreatedOn.ToString(),
					LikesCount = post.Likes.Count,
					Tags = post.Tags.Select(t => t.Tag.Name).ToList(),
					Content = post.Content,
					Comments = comments,
					User = user
				};

				return View(model);
			}
			catch (EntityNotFoundException e)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult Create()
		{
			if (!this.HttpContext.Session.Keys.Contains("LoggedUser"))
			{
				return RedirectToAction("Login", "User");
			}
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

				//TODO: Refactor after Authentication.
				string userName = HttpContext.Session.GetString("LoggedUser");
				var newPost = postService.CreatePost(pendingPost, userName);

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

		[HttpGet]
		public IActionResult DeleteSuccessful()
		{
			return View();
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult Delete([FromRoute] int id)
		{
			try
			{
				//int userId = HttpContext.Session.GetInt32("userId") ?? 0;
				//var user = userService.GetUserById(userId);
				int roleId = (int)HttpContext.Session.GetInt32("roleId");
				string loggedUser = HttpContext.Session.GetString("LoggedUser");
				if (id == 0)
				{
					throw new EntityNotFoundException("Entity not found.");
				}

				if (authManager.AdminCheck(roleId) || !authManager.BlockedCheck(roleId))
				{
					var isDeleted = postService.DeletePostById(id, loggedUser);
					return RedirectToAction("DeleteSuccessful", "Post");
				}

				throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
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
