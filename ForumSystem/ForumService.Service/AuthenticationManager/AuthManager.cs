using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.AuthenticationManager
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserService userService;

        public AuthManager(IUserService userService)
        {
            this.userService = userService;
        }

        public User UserCheck(string credentials)
        {
            if(credentials is null) throw new UnauthenticatedOperationException("Please enter credentials!");
            string[] usernameAndPassword = credentials.Split(':');
            string userName = usernameAndPassword[0];
            string password = usernameAndPassword[1];

            string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            var user = userService.GetUserByUserName(userName);
            if (user.Password == encodedPassword)
            {
                return user;
            }

            throw new UnauthenticatedOperationException("Invalid username or password!");

        }

        public void AdminCheck(string credentials)
        {
            var user = UserCheck(credentials);
            if (user.RoleId != 3)
            {
                throw new UnauthorizedAccessException("You'rе not admin!");
            }
            
        }

        public void BlockedCheck(string credentials)
        {
            var user = UserCheck(credentials);
            if (user.RoleId == 1)
            {
                throw new UnauthorizedAccessException("You'rе blocked, can't perform this action");
            }

        }
    }
}
