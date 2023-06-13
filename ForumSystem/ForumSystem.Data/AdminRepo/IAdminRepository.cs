using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.AdminRepo
{
    public interface IAdminRepository
    {
        void MakeUserAdmin(User user);

        void BlockUser(User user);

        void UnBlockUser(User user);
    }
}
