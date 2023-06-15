using ForumSystemDTO.UserDTO;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.Api.QueryParams;

namespace ForumSystem.Business.UserService
{
    public interface IUserService
    {
        User CreateUser(CreateUserDTO user);
        IEnumerable<User> GetAllUsers();

        ICollection<Post> GetUserPosts(PostQueryParameters queryParams, int id);


        User GetUserById(int userId);
        public User GetUserByEmail(string email);
      
        User GetUserByUserName(string userName);

        List<User> SearchBy(UserQueryParams queryParams);

        User UpdateUser(string username, User user);

        bool DeleteUser(string userName,int? userId);


    }
}
