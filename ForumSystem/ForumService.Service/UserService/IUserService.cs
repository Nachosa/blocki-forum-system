using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.UserService
{
    public interface IUserService
    {
        public IEnumerable<User> GetAllUsers();

        public User CreateUser(User user);

        public bool UpdateUser(int userId, User user);

        public void DeleteUser(int Id);

        public User FindUserById(int userId);
    }
}
