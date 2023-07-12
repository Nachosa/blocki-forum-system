using AutoMapper;
using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.TagService;
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
        private readonly ITagService tagService;

        public PostController(IPostService postService, IMapper mapper, IUserService userService, IAuthManager authManager, IAuthorizator authorizator, ITagService tagService)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.userService = userService;
            this.authManager = authManager;
            this.authorizator = authorizator;
            this.tagService = tagService;
        }

		public IActionResult Index(FilterPosts filterParams)
		{
			try
			{
                var parameters = mapper.Map<PostQueryParameters>(filterParams);

				var result = new List<Post>();

				//if (!string.IsNullOrEmpty(filterValue))
				//{
				//	switch (filterBy)
				//	{
				//		case "author":
				//			parameters.CreatedBy = filterValue;

				//			break;
				//		case "tags":
				//			parameters.Tag = filterValue;

				//			break;
				//		case "title":
				//			parameters.Title = filterValue;

				//			break;
				//		default:
				//			break;
				//	}
				//}

    //            if (endDate.HasValue && startDate.HasValue)
    //            {
				//	parameters.MaxDate = endDate;
				//	parameters.MinDate = startDate;
				//}
    //            else if (startDate.HasValue)
    //            {
    //                parameters.MinDate = startDate;
    //            }

    //            parameters.SortBy = sortBy;
				//parameters.SortOrder = sortOrder;

				result = postService.GetPosts(parameters);
				var postViewModels = result.Select(p => mapper.Map<PostViewModelAbbreviated>(p)).ToList();

				//ViewBag.Title = Title;
				//ViewBag.FilterBy = filterBy;
				//ViewBag.SortBy = sortBy;
				//ViewBag.StartDate = startDate;

				// Pagination logic
				var currentPage = filterParams.Page ?? 1;
				var pageSize = 5;
				var totalPosts = postViewModels.Count;

				var totalPages = (int) Math.Ceiling(totalPosts / (double) pageSize);

				ViewBag.CurrentPage = currentPage;
				ViewBag.TotalPages = totalPages;
				ViewBag.TotalPosts = totalPosts;

				// Apply pagination
				postViewModels = postViewModels.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                filterParams.Posts = postViewModels;

				return View(filterParams);
			}
			catch (Exception e)
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
		}

		//[HttpGet]
		//public IActionResult Index(string filterBy, string sortBy, string sortOrder, int? page)
		//{
		//	try
		//	{
		//		var parameters = new PostQueryParameters();
		//		var result = new List<Post>();

		//		if (!string.IsNullOrEmpty(filterBy))
		//		{
  //                  parameters.CreatedBy = filterBy;
		//			parameters.Title = filterBy;
		//		}

		//		if (!string.IsNullOrEmpty(sortBy))
		//		{
		//			parameters.SortBy = sortBy;
		//		}

		//		if (!string.IsNullOrEmpty(sortOrder))
		//		{
		//			parameters.SortOrder = sortOrder;
		//		}

		//		result = postService.GetPosts(parameters);

		//		var postViewModels = result.Select(p => mapper.Map<PostViewModelAbbreviated>(p)).ToList();

		//		ViewBag.FilterBy = filterBy;
		//		ViewBag.SortBy = sortBy;

		//		// Pagination logic
		//		var totalPosts = postViewModels.Count;
		//		var pageSize = 5; // Number of posts per page
		//		var totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize);
		//		var currentPage = page ?? 1;

		//		ViewBag.TotalPosts = totalPosts;
		//		ViewBag.TotalPages = totalPages;
		//		ViewBag.CurrentPage = currentPage;

		//		// Apply pagination
		//		postViewModels = postViewModels.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

		//		return View(postViewModels);
		//	}
		//	catch (Exception e)
		//	{
		//		// TODO: More precise exception handling and status code.
		//		Response.StatusCode = StatusCodes.Status400BadRequest;
		//		ViewData["ErrorMessage"] = e.Message;
		//		return View("Error");
		//	}
		//}

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
        public IActionResult LikePost([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                string loggedUsername = this.HttpContext.Session.GetString("LoggedUser");
                _ = userService.GetUserByUserName(loggedUsername);
                postService.LikePost(id, loggedUsername);
                return RedirectToAction("PostDetails", "Post", new { Id = id });

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }


        }
        [HttpGet]
        public IActionResult DislikePost([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                string loggedUsername = this.HttpContext.Session.GetString("LoggedUser");
                _ = userService.GetUserByUserName(loggedUsername);
                postService.DislikePost(id, loggedUsername);
                return RedirectToAction("PostDetails", "Post", new { Id = id });

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
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

        [HttpPost, ActionName("DeletePost")]
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
            var editPostForm = mapper.Map<EditPostViewModel>(post);
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
                    var loggedUserName = this.HttpContext.Session.GetString("LoggedUser");
					// Add tags to the post
					tagService.AddTagsToPost(loggedUser,id, postEdits.Tags);

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
