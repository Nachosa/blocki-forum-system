﻿using ForumSystem.Business.Abstractions;
using ForumSystem.Business.Models;

namespace ForumSystem.DataAccess
{
    public class ForumSystemRepository : IForumSystemRepository
    {
        public static IList<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Gosho",
                LastName = "Goshev",
                Username = "goshoXx123",
                Email = "gosho@gmail.com",
                Password = "1234567890"
            }
        };
        public static IList<Post> posts = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                UserId = 1,
                Title = "Title",
                Content = "Content"
            }
        };

        public IEnumerable<Post> GetAllPosts()
        {
            return new List<Post>(posts);
        }
    }
}
