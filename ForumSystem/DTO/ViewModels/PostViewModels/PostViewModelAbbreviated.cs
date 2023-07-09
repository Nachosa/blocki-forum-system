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
		public string Title { get; set; }
		public string CreatedBy { get; set; }
		public User User { get; set; }
		public string CreatedOn { get; set; }
		public int LikesCount { get; set; }
		public int DisikesCount { get; set; }
		public List<string> Tags { get; set; }
		public int CommentsCount { get; set; }
		public int PostId { get; set; }
	}
}
