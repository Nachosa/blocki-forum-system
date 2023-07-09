using ForumSystem.DataAccess.Models;
using ForumSystemDTO.CommentDTO;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.PostDTO
{
    public class GetPostDto
    {
        [MinLength(16, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(64, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Title { get; set; }

        [MinLength(32, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(8192, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Content { get; set; }

        public string Username { get; set; }

        public int LikesCount { get; set; }

		public int DislikesCount { get; set; }

		public DateTime CreatedOn { get; set; }

        public ICollection<string> Tags { get; set; } = new List<string>();

        public ICollection<GetCommentDto> Comments { get; set; } = new List<GetCommentDto>();
    }
}
