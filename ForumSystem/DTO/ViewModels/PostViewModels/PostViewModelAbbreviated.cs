using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.CommentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.PostViewModels
{
	public class PostViewModelAbbreviated
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string CreatedBy { get; set; }
		public User User { get; set; }
		public DateTime CreatedOn { get; set; }
		public int LikesCount { get; set; }
		public int DislikesCount { get; set; }
		public List<PostTag> Tags { get; set; }
		public int CommentsCount { get; set; }
	}
}
