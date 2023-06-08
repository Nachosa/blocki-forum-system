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
        IEnumerable<GetUserDTO> GetAllUsers();

        User CreateUser(CreateUserDTO user);

        GetUserDTO UpdateUser(int userId, UpdateUserDTO user);

        bool DeleteUser(int Id);

        GetUserDTO GetUserById(int userId);

        IEnumerable<GetUserDTO> GetUsersByFirstName(string firstName);

        GetUserDTO GetUserByEmail(string email);

        GetUserDTO GetUserByUserName(string userName);
    }
}
