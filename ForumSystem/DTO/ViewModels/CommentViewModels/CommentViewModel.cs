using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.CommentViewModels
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		public string CommentContent { get; set; }

		public string UserName { get; set; }
	}
}
