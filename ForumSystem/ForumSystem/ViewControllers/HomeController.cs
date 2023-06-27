using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        public HomeController(IUserService userService,IPostService postService,ICommentService commentService)
        {
            this.userService = userService;
            this.postService = postService;
            this.commentService = commentService;
        }
        public IActionResult Index()
        {
            int activeUsersCount = userService.GetUsersCount();
            int activePostsCount = postService.GetPostsCount();
            PostQueryParameters postQueryparams;
            postQueryparams.SortBy
            List<Post> mostCommentedPosts = postService.GetPostsCount( );
            List<Post> recentlyCreatedPosts= new List<Post>();
            return View();
        }
    }
}
