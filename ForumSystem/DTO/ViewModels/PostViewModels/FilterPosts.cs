using ForumSystem.Api.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.PostViewModels
{
	public class FilterPosts : PostQueryParameters
	{
		public int? Page { get; set; }
		public List<PostViewModelAbbreviated> Posts { get; set; }
	}
}
