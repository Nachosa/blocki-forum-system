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
        User CheckUser(string credentials);

        void IsAdmin(string credentials);


    }
}
