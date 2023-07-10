using AutoMapper;
using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.DataAccess.UserRepo;
using ForumSystemDTO.ViewModels.CommentViewModels;
using ForumSystemDTO.ViewModels.HomeViewModels;
using ForumSystemDTO.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
	public class HomeController : Controller
	{
		private readonly IPostService postService;
		private readonly IUserService userService;
		private readonly IMapper mapper;

		public HomeController(IPostService postService, IUserService userService, IMapper mapper)
		{
			this.postService = postService;
			this.userService = userService;
			this.mapper = mapper;
		}

		public IActionResult Index()
		{
			int activeUsersCount = userService.GetUsersCount();
			int activePostsCount = postService.GetPostsCount();

			PostQueryParameters postQueryparams = new PostQueryParameters();
			postQueryparams.SortBy = "comments";
			postQueryparams.SortOrder = "desc";
			List<PostViewModelAbbreviated> topCommentedPosts = postService.GetPosts(postQueryparams).Take(10).Select(p => mapper.Map<PostViewModelAbbreviated>(p)).ToList();
			postQueryparams.SortBy = "date";
			postQueryparams.SortOrder = "desc";
			List<PostViewModelAbbreviated> recentlyCreatedPosts = postService.GetPosts(postQueryparams).Take(10).Select(p => mapper.Map<PostViewModelAbbreviated>(p)).ToList();

			HomePageViewModel homePage = new HomePageViewModel();
			homePage.UsersCount = activeUsersCount;
			homePage.PostsCount = activePostsCount;
			homePage.TopCommentedPosts = topCommentedPosts;
			homePage.RecentlyCreatedPosts = recentlyCreatedPosts;
			return View(homePage);
		}

		[HttpGet]
		public IActionResult Search(string input)
		{
			var results = new SearchViewModel();

			try
			{//Searching for user with that username 
				results.UserWithUsername = userService.GetUserByUserName(input);
			}
			catch (EntityNotFoundException)
			{
				results.UserWithUsername = null;
			}
			catch (Exception e)
			{
				this.Response.StatusCode = StatusCodes.Status500InternalServerError;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}

			var postParams = new PostQueryParameters();

			try
			{//Searching for posts which contain that input in their title
				postParams.Title = input;
				results.PostsContaingThatInput = postService.GetPosts(postParams);
			}
			catch (Exception e)
			{
				this.Response.StatusCode = StatusCodes.Status500InternalServerError;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}

			try
			{//Searching for posts which have that input like a tag
				postParams.Title = null;
				postParams.Tag = input;
				results.PostsWithTag = postService.GetPosts(postParams);
			}
			catch (Exception e)
			{
				this.Response.StatusCode = StatusCodes.Status500InternalServerError;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}

			return View(results);
		}
		[HttpGet]
		public IActionResult Login()
		{
			return RedirectToAction("Login", "User");
		}

		[HttpGet]
		public IActionResult About()
		{
			return View();
		}
	}
}
