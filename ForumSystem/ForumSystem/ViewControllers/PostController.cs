using AutoMapper;
using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.Web.Helpers;
using ForumSystem.Web.Helpers.Contracts;
using ForumSystemDTO.PostDTO;
using ForumSystemDTO.ViewModels.CommentViewModels;
using ForumSystemDTO.ViewModels.PostViewModels;
using ForumSystemDTO.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ForumSystem.Web.ViewControllers
{
	public class PostController : Controller
	{
		private readonly IPostService postService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
		private readonly IAuthManager authManager;
		private readonly IAuthorizator authorizator;

		public PostController(IPostService postService, IMapper mapper, IUserService userService, IAuthManager authManager, IAuthorizator authorizator)
		{
			this.postService = postService;
			this.mapper = mapper;
			this.userService = userService;
			this.authManager = authManager;
			this.authorizator = authorizator;
		}

		[HttpGet]
		public IActionResult Index(string filterBy, string sortBy, string sortOrder)
		{
			try
			{
				var parameters = new PostQueryParameters();
				var result = new List<Post>();

				if (!string.IsNullOrEmpty(filterBy))
				{
					parameters.Content = filterBy;
					// parameters.Tag = filterBy;
					parameters.Title = filterBy;
				}

				if (!string.IsNullOrEmpty(sortBy))
				{
					parameters.SortBy = sortBy;
				}

				if (!string.IsNullOrEmpty(sortOrder))
				{
					parameters.SortOrder = sortOrder;
				}

				result = postService.GetPosts(parameters);

				var postViewModels = result.Select(p => mapper.Map<PostViewModelAbbreviated>(p)).ToList();

				ViewBag.FilterBy = filterBy;
				ViewBag.SortBy = sortBy;

				return View(postViewModels);
			}
			catch (Exception e)
			{
				// TODO: More precise exception handling and status code.
				Response.StatusCode = StatusCodes.Status400BadRequest;
				ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult PostDetails(int id)
		{
			try
			{
				var post = postService.GetPostById(id);
				var user = userService.GetUserById(post.UserId);
				var model = mapper.Map<PostDetailsViewModel>(post);

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
			if (!authorizator.isLogged("LoggedUser"))
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
				//Има ли нужда от проверка тук?
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
				if (!this.ModelState.IsValid)
				{
					return View(createPostFormFilled);
				}

				int roleId = (int)HttpContext.Session.GetInt32("roleId");
				string loggedUser = HttpContext.Session.GetString("LoggedUser");

				if (authManager.AdminCheck(roleId) || !authManager.BlockedCheck(roleId))
				{
					var createPostFormMapped = mapper.Map<Post>(createPostFormFilled);
					var createdPost = postService.CreatePost(createPostFormMapped, loggedUser);
					return RedirectToAction("PostDetails", "Post", new { id = createdPost.Id });
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

		[HttpGet]
		public IActionResult DeleteSuccessful()
		{
			return View();
		}

		[HttpGet]
		public IActionResult DeletePost([FromRoute] int id)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
				//Взимам post променливата за да мода да сверя нейният UserId с този на логнатият User.
				var post = postService.GetPostById(id);
				if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", post.UserId))
				{
					this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
					return View("Error");
				}
				if (authorizator.isBlocked("roleId"))
				{
					throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
				}
				this.ViewBag.postIdToDelete = id;
                return View();

			}
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
            catch (UnauthorizedAccessException e)
			{
				this.Response.StatusCode = StatusCodes.Status403Forbidden;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
			catch (Exception e)
			{
				//TODO: More precise exception handling and status code.
				this.Response.StatusCode = StatusCodes.Status400BadRequest;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}

		[HttpPost,ActionName("DeletePost")]
		public IActionResult Delete([FromRoute] int id)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
				//Взимам post променливата за да мода да сверя нейният UserId с този на логнатият User.
				var post = postService.GetPostById(id);
				if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", post.UserId))
				{
					this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
					return View("Error");
				}
				int roleId = (int)HttpContext.Session.GetInt32("roleId");
				string loggedUser = HttpContext.Session.GetString("LoggedUser");
				if (id == 0)
				{
					throw new EntityNotFoundException("Entity not found.");
				}

				if (authManager.AdminCheck(roleId) || !authManager.BlockedCheck(roleId))
				{
					_ = postService.DeletePostById(id, loggedUser);
					return View("DeleteSuccessful");
				}

				throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
			}
			catch (Exception e)
			{
				//TODO: More precise exception handling and status code.
				this.Response.StatusCode = StatusCodes.Status400BadRequest;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult Edit([FromRoute] int id)
		{
			if (!authorizator.isLogged("LoggedUser"))
			{
				return RedirectToAction("Login", "User");
			}
			var post = postService.GetPostById(id);
			if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", post.UserId))
			{
				this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
				this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
				return View("Error");
			}
			var currPost = postService.GetPostById(id);
			var editPostForm = mapper.Map<EditPostViewModel>(currPost);
			return View(editPostForm);
		}

		[HttpPost]
		public IActionResult Edit([FromRoute] int id, EditPostViewModel postEdits)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
				//Най-вероятно и тук се нуждаем от проверка дали, заявката идва от автора/админ.
				if (!this.ModelState.IsValid)
				{
					return View(postEdits);
				}

				int roleId = (int)HttpContext.Session.GetInt32("roleId");
				string loggedUser = HttpContext.Session.GetString("LoggedUser");

				if (id == 0)
				{
					throw new EntityNotFoundException("Entity not found.");
				}
				if (authManager.AdminCheck(roleId) || !authManager.BlockedCheck(roleId))
				{
					var postEditsMapped = mapper.Map<Post>(postEdits);
					var updatedPost = postService.UpdatePostContent(id, postEditsMapped, loggedUser);
					return RedirectToAction("PostDetails", "Post", new { id });
				}

				throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
			}
			catch (Exception e)
			{
				//TODO: More precise exception handling and status code.
				this.Response.StatusCode = StatusCodes.Status400BadRequest;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}


	}
}
