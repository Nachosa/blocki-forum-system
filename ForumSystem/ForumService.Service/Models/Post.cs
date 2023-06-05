using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Business.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [MinLength(16, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(64, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Title { get; set; }

        [MinLength(32, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(8192, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
