using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.Helper
{
    public class CreateUserMapper
    {
        public User Map(CreateUserDTO userDTO)
        {
            var user = new User();
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;   
            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            user.Role = Role.User;
            return user;
        }
    }
}
