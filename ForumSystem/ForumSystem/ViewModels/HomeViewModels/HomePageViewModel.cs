using ForumSystem.DataAccess.Models;

namespace ForumSystem.Web.ViewModels.HomeViewModels
{
    public class HomePageViewModel
    {
        public int UsersCount { get; set; }

        public int PostsCount { get; set; }

        //list of the top 10 most commented posts
        public List<Post> TopCommentedPosts { get; set; }= new List<Post>();

        //list of the 10 most recently created posts
        public List<Post> RecentlyCreatedPosts { get; set; } = new List<Post>();

    }
}
