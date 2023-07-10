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
		public int Id { get; set; }
		public string Title { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int LikesCount { get; set; }
		public int DislikesCount { get; set; }
		public List<string> Tags { get; set; }
		public string Content { get; set; }
		public List<CommentViewModel> Comments { get; set; }
		public List<Like> Likes { get; set; }
		public User User { get; set; }
	}
}
