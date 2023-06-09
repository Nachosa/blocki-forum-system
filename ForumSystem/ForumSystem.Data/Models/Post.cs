using System.ComponentModel.DataAnnotations;

namespace ForumSystem.DataAccess.Models
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

        public int Likes { get; set; } = 0;

        public int Dislikes { get; set; } = 0;

        public static int Count { get; set; } = 0;

        public DateTime CreatedOn { get; set; } = DateTime.Now;


        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Post other = (Post)obj;
            return Id == other.Id; // Compare based on the unique ID property
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
