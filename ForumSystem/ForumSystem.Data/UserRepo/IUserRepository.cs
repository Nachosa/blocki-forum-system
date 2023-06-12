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
        IEnumerable<User> GetAllUsers();

        User GetUserById(int userId);

        User GetUserByUserName(string userName);

        List<User> Searchby(UserQueryParams queryParams);

        User CreateUser(User user);

        User UpdateUser(User user);

        bool DeleteUser(User user);
    }
}
