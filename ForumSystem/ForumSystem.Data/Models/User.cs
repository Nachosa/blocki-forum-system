using System.ComponentModel.DataAnnotations;

namespace ForumSystem.DataAccess.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MinLength(4, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string FirstName { get; set; }

        [MinLength(4, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string LastName { get; set; }

        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public int? PhoneNumber { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public Role Role { get; set; } = Role.User;
    }
}
