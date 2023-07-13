using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.PostViewModels;
using ForumSystemDTO.ViewModels.CommentViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.UserViewModels
{
	public class UserDetailsViewModel
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Username { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		[JsonIgnore]
		public ICollection<PostViewModelAbbreviated> Posts { get; set; }

		public ICollection<CommentViewModel> Comments { get; set; }

		public int LikesCount { get; set; }

		public int DislikesCount { get; set; }

		public int RoleId { get; set; }

		public string ProfilePicPath { get; set; }
	}
}
