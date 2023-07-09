using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.PostViewModels;

namespace ForumSystemDTO.ViewModels.HomeViewModels
{
    public class HomePageViewModel
    {
        public int UsersCount { get; set; }

        public int PostsCount { get; set; }

        //list of the top 10 most commented posts
        public List<PostViewModelAbbreviated> TopCommentedPosts { get; set; }= new List<PostViewModelAbbreviated>();

        //list of the 10 most recently created posts
        public List<PostViewModelAbbreviated> RecentlyCreatedPosts { get; set; } = new List<PostViewModelAbbreviated>();
    }
}
