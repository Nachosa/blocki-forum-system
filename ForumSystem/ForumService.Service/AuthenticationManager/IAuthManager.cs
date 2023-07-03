using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.AuthenticationManager
{
    public  interface IAuthManager
    {
        User UserCheck(string credentials);

        User UserCheck(string userName, string password);

		void AdminCheck(string credentials);

        bool AdminCheck(User user);

        void BlockedCheck(string credentials);

        bool BlockedCheck(User user);

    }
}
