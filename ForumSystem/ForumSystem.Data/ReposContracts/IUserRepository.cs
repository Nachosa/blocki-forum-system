using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.ReposContracts
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();

        public User FindUserById(int userId);

        public User CreateUser(User user);

        public bool UpdateUser(User user);

        public void DeleteUser(User user);
    }
}
