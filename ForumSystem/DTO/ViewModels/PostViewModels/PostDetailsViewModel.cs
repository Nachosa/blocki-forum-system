using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystemDTO.ViewModels.CommentViewModels;

namespace ForumSystemDTO.ViewModels.PostViewModels
{
	public class PostDetailsViewModel
	{
		public string Title { get; set; }
		public string CreatedBy { get; set; }
		public string CreatedOn { get; set; }
		public int LikesCount { get; set; }
		public List<string> Tags { get; set; }
		public string Content { get; set; }
		public List<CommentViewModel> Comments { get; set; }
		public int PostId { get; set; }

		public User User { get; set; }
	}
}
