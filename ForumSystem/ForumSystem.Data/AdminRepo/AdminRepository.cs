using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.AdminRepo
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ForumSystemContext forumDb;
        public AdminRepository(ForumSystemContext forumDb)
        {
            this.forumDb = forumDb;
        }
        public void BlockUser(User user)
        {
            user.RoleId = 1;
            forumDb.SaveChanges();
        }

        public void MakeUserAdmin(User user)
        {
            user.RoleId = 3;
            forumDb.SaveChanges();
        }

        public void UnBlockUser(User user)
        {
            user.RoleId = 2;
            forumDb.SaveChanges();
        }
    }
}
