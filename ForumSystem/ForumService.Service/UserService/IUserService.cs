using ForumSystemDTO.UserDTO;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QuerryParams;
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

        List<GetUserDTO> SearchBy(UserQueryParams queryParams);


    }
}
