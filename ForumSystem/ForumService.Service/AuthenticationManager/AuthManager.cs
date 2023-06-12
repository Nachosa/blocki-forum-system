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

        public User CheckUser(string credentilas)
        {
            string[] usernameAndPassword = credentilas.Split(':');
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

        public bool IsAdmin(string credentials)
        {
            throw new NotImplementedException();
        }
    }
}
