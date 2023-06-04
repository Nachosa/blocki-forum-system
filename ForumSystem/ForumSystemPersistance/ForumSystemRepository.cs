using ForumSystem.Models;
namespace ForumSystemPersistance
{
    public class ForumSystemRepository
    {
        public static IList<User> users = new List<User>()
        {
            new User()
            {
                firstName = "Gosho",
                lastName = "Goshev",
                username = "goshoXx123",
                email = "gosho@gmail.com",
                password = "1234567890"
            }
        };
        public static IList<Post> Users = new List<Post>()
        {
            new Post() 
            {
                user = users.First(),
                title = "Title",
                content = "Content"
            }
        };
    }
}
