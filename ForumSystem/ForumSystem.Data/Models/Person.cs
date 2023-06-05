using ForumSystem.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Business.Models
{
    public abstract class Person
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

        public Role Role { get; set; }
    }
}
