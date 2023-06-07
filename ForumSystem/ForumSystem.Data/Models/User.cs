using System.ComponentModel.DataAnnotations;

namespace ForumSystem.DataAccess.Models
{
    public class User : Person
    {
        public bool Restricted { get; set; } = false;

        public Role Role { get; set; } = Role.User;

    }
}
