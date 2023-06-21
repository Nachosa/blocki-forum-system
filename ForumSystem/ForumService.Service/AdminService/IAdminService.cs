using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.AdminService
{
    public interface IAdminService
    {
        bool MakeUserAdmin(int? id, string email);

        bool BlockUser(int? id, string email);

        bool UnBlockUser(int? id, string email);

        bool DeletePost(int? id);

    }
}
