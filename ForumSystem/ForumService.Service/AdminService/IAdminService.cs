using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.AdminService
{
    public interface IAdminService
    {
        void MakeUserAdmin(int? id, string email);

        void BlockUser(int? id, string email);

        void UnBlockUser(int? id, string email);

    }
}
