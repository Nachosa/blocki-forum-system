using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.UserRepo
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(int userId);

        IEnumerable<User> GetUsersByFirstName(string firstName);

        User GetUserByEmail(string email);

        User GetUserByUserName(string UserName);

        User CreateUser(User user);

        User UpdateUser(User user);

        bool DeleteUser(User user);
    }
}
