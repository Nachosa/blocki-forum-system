using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.PostViewModels;
using ForumSystemDTO.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.HomeViewModels
{
	public class SearchViewModel
	{
		public string input { get; set; }
		public List<PostViewModelAbbreviated> PostsWithTag { get; set; } = new List<PostViewModelAbbreviated>();

		public List<UserDetailsViewModel> UsersWhichContainInput { get; set; } = new List<UserDetailsViewModel>();

		public List<PostViewModelAbbreviated> PostsWithTitle { get; set; } = new List<PostViewModelAbbreviated>();
	}
}
