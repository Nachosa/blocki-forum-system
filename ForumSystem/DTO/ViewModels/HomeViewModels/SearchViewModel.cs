using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.HomeViewModels
{
	public class SearchViewModel
	{
		public List<Post> PostsWithTag { get; set; }

		public User UserWithUsername { get; set; }

		public List<Post> PostsContaingThatInput { get; set; }
	}
}
