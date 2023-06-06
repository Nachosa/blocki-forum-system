using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.UserService
{
    public class UserService : IUserService
    {
        private readonly IForumSystemRepository repo;
        public UserService()
        {
            repo = new ForumSystemRepository();
        }
        public User CreateUser(User user)
        {
            return repo.CreateUser(user);
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = repo.FindUserById(userId);
            if (userToDelete is null)
            {
                throw new Exception($"User with Id={userId} was not found!");
            }
            repo.DeleteUser(userToDelete);
        }

        public User FindUserById(int userId)
        {
            var user = repo.FindUserById(userId);
            return user ?? throw new Exception($"User with Id={userId} was not found!");
        }

        public IEnumerable<User> GetAllUsers()
        {
            var allUsers = repo.GetAllUsers();
            bool anyUsers = allUsers.Any();
            if (anyUsers == false)
            {
                throw new Exception("There aren't any users yet!");
            }
            return allUsers;
        }

        public bool UpdateUser(int userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
