namespace ForumSystem.Models
{
    public class User
    {
        public string firstName;
        public string lastName;
        public string email;
        public string password;
        public IList<Comment> comments;
        public int likes;

    }
}
