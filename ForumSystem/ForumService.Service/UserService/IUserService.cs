using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.Business.CreateUpdateGet_UserDTO;
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
        public IEnumerable<GetUserDTO> GetAllUsers();

        public User CreateUser(CreateUserDTO user);

        public bool UpdateUser(int userId, UpdateUserDTO user);

        public void DeleteUser(int Id);

        public GetUserDTO FindUserById(int userId);
    }
}
