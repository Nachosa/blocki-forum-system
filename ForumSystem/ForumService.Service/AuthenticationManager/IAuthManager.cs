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

        void AdminCheck(string credentials);

        public void BlockedCheck(string credentials);


    }
}
