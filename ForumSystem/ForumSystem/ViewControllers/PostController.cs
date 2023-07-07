using AutoMapper;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.Web.Helpers;
using ForumSystem.Web.Helpers.Contracts;
using ForumSystemDTO.ViewModels.CommentViewModels;
using ForumSystemDTO.ViewModels.PostViewModels;
using ForumSystemDTO.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
	public class PostController : Controller
	{
		private readonly IPostService postService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
		private readonly IAuthManager authManager;
		private readonly IAuthorizator authorizator;

		public PostController(IPostService postService, IMapper mapper, IUserService userService, IAuthManager authManager,IAuthorizator authorizator)
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
				var posts = postService.GetAllPosts();

				if (!string.IsNullOrEmpty(filterBy))
				{
					posts = posts.Where(p => p.Title.ToLower().Contains(filterBy.ToLower())).ToList();
				}

				if (!string.IsNullOrEmpty(sortBy))
				{
					switch (sortBy)
					{
						case "title":
							posts = (sortOrder == "desc") ? posts.OrderByDescending(p => p.Title).ToList() : posts.OrderBy(p => p.Title).ToList();
							break;

							//posts = posts.OrderBy(p => p.Title).ToList();
							//break;
						case "date":
							posts = (sortOrder == "desc") ? posts.OrderByDescending(p => p.CreatedOn).ToList() : posts.OrderBy(p => p.CreatedOn).ToList();
							break;

							//posts = posts.OrderBy(p => p.CreatedOn).ToList();
							//break;
						// Add more sorting options as needed
						default:
							// Default sorting option
							posts = posts.OrderBy(p => p.CreatedOn).ToList();
							break;
					}
				}

				var postViewModels = posts.Select(p => new PostDetailsViewModel
				{
					PostId = p.Id,
					Title = p.Title,
					CreatedBy = p.User.Username,
					CreatedOn = p.CreatedOn.ToString(),
					LikesCount = p.Likes.Count,
					Tags = p.Tags.Select(t => t.Tag.Name).ToList(),
					Content = p.Content,
					Comments = p.Comments
						.Where(c => !c.IsDeleted)
						.Select(c => new CommentViewModel
						{
							CommentContent = c.Content,
							Id = c.Id,
							UserName = c.User?.Username ?? "Anonymous"
						}).ToList(),
					User = p.User
				}).ToList();

				// Pass filterBy and sortBy values to the view
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

				var comments = post.Comments
					.Where(c => !c.IsDeleted)
					.Select(c => new CommentViewModel
				{
					CommentContent = c.Content,
					Id = c.Id,
					UserName = c.User?.Username ?? "Anonymous" // provide a fallback value if the User is null
				}).ToList();

				//var comments = post.Comments.Select(c => new CommentViewModel
				//{
				//	CommentContent = c.Content,
				//	Id = c.Id,
				//	UserName = c.User?.Username ?? "Anonymous" // provide a fallback value if the User is null
				//}).ToList();

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

		[HttpPost, ActionName("Delete")]
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
					return RedirectToAction("DeleteSuccessful", "Post");
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
            var editPostForm = new EditPostViewModel();
			return View(editPostForm);
		}

		[HttpPost, ActionName("Edit")]
		public IActionResult Edit([FromRoute] int id, EditPostViewModel postEdits)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
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
					var createdPost = postService.UpdatePostContent(id, postEditsMapped, loggedUser);
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
