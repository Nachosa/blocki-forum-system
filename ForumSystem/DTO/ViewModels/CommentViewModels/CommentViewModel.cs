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

		public int PostId { get; set; }

		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }

		public string Username { get; set; }
		//Трябва да има и дата.
	}
}
