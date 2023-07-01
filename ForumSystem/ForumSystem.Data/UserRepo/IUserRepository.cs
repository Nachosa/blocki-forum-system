using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.UserRepo
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        IEnumerable<User> GetAllUsers();

        User GetUserById(int userId);

        User GetUserByUserName(string userName);
        User GetUserByEmail(string email);

        int GetUsersCount();

        public IEnumerable<User> GetUsersByFirstName(string firstName);
     
        List<User> SearchBy(UserQueryParams queryParams);

        User UpdateUser(string userName,User user);

        public User UpdateUser(int id, User user);

        bool MakeUserAdmin(User user);

        bool EmailExist(string email);

        bool DeleteUser(User user);
    }
}
