namespace ForumSystem.DataAccess.Models
{
    public class Admin : Person
    {
        public int PhoneNumber { get; set; }

        public Role Role { get; set; } = Role.Admin;

    }
}
