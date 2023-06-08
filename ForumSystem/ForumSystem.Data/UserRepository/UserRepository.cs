using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;

namespace ForumSystem.DataAccess.UserRepository
{
    public class UserRepository : IUserRepository
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
                Password = "1234567890",
                Role = Role.User
            },
            new User()
            {
                Id = 2,
                FirstName = "Nikolai",
                LastName = "Barekov",
                Username = "BarekaXx123",
                Email = "Nikolai@gmail.com",
                Password = "1234567890",
                Role = Role.User

            },
            new User()
            {
                Id = 3,
                FirstName = "Boiko",
                LastName = "Borisov",
                Username = "BokoMoko",
                Email = "gosho@gmail.com",
                Password = "1234567890",
                Role = Role.User

            },
            new User()
            {
                Id = 4,
                FirstName = "Cvetan",
                LastName = "Cvetanov",
                Username = "Cvete123",
                Email = "Cvetan@gmail.com",
                Password = "1234567890",
                Role = Role.User

            },
            new User()
            {
                Id = 5,
                FirstName = "Kosta",
                LastName = "Kopeikin",
                Username = "BrainDamage123",
                Email = "Kopeikin@gmail.com",
                Password = "1234567890",
                Role = Role.User

            }
        };

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public User CreateUser(User user)
        {
            user.Id = users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            users.Add(user);
            return user;
        }

        public bool UpdateUser(User user)
        {
            var userToupdate = users.FirstOrDefault(u => u.Id == user.Id);
            userToupdate.FirstName = user.FirstName ?? userToupdate.FirstName;
            userToupdate.LastName = user.LastName ?? userToupdate.LastName;
            userToupdate.Email = user.Email ?? userToupdate.Email;
            userToupdate.Password = user.Password ?? userToupdate.Password;
            return true;
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public User GetUserById(int Id)
        {
            var user = users.FirstOrDefault(u => u.Id == Id);
            return user;
        }

        public IEnumerable<User> GetUsersByFirstName(string firstName)
        {
            var usersWithThatName = users.Where(u => u.FirstName == firstName);
            return usersWithThatName;
        }

        public User GetUserByEmail(string email)
        {
            var userWithThatEmail = users.FirstOrDefault(u => u.Email == email);
            return userWithThatEmail;
        }

        public User GetUserByUserName(string UserName)
        {
            var userWithThatUserName = users.FirstOrDefault(u => u.Username == UserName);
            return userWithThatUserName;
        }
    }
}
