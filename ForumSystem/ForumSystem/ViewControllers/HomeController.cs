using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
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

        public HomeController(IPostService postService, IUserService userService)
        {
			this.postService = postService;
			this.userService = userService;
        }

        public IActionResult Index()
        {
            int activeUsersCount = userService.GetUsersCount();
            int activePostsCount = postService.GetPostsCount();

            PostQueryParameters postQueryparams= new PostQueryParameters();
            postQueryparams.SortBy = "comments";
            postQueryparams.SortOrder = "desc";
            List<Post> topCommentedPosts = postService.GetPosts(postQueryparams).Take(10).ToList();
            postQueryparams.SortBy = "date";
            postQueryparams.SortOrder = "desc";
            List<Post> recentlyCreatedPosts = postService.GetPosts(postQueryparams).Take(10).ToList();

            HomePageViewModel homePage = new HomePageViewModel();
            homePage.UsersCount= activeUsersCount;
            homePage.PostsCount= activePostsCount;
            homePage.TopCommentedPosts= topCommentedPosts;
            homePage.RecentlyCreatedPosts= recentlyCreatedPosts;
            return View(homePage);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
	}
}
