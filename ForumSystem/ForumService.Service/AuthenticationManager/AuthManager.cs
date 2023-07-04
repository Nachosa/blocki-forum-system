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

            var user = userService.GetUserByUserName(userName);
            if (user.Password == password)
            {
                return user;
            }

            throw new UnauthenticatedOperationException("Invalid username or password!");
        }

		public User UserCheck(string userName ,string password)
		{
			var user = userService.GetUserByUserName(userName);
			if (user.Password == password)
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
        public bool AdminCheck(User user)
        {
            if (user.RoleId == 3)
            {
                return true;
            }
            return false;
        }

		public bool AdminCheck(int roleId)
		{
			if (roleId == 3)
			{
				return true;
			}
			return false;
		}
        public void BlockedCheck(string credentials)
        {
            var user = UserCheck(credentials);
            if (user.RoleId == 1)
            {
                throw new UnauthorizedAccessException("You'rе blocked, can't perform this action");
            }

        }
        public bool BlockedCheck(User user)
        {
            if (user.RoleId == 1)
            {
                return true;
            }
            return false;
        }


		public bool BlockedCheck(int roleId)
		{
			if (roleId == 1)
			{
				return true;
			}
			return false;
		}
	}
}
