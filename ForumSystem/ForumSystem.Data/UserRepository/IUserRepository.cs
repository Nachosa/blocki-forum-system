using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.UserRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(int userId);

        IEnumerable<User> GetUsersByFirstName(string firstName);

        User GetUserByEmail(string email);

        User GetUserByUserName(string UserName);

        User CreateUser(User user);

        bool UpdateUser(User user);

        void DeleteUser(User user);
    }
}
