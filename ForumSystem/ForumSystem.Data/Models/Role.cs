using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
    public enum Roles
    {
        Blocked = 1,
        User = 2,
        Admin = 3
    }

    public class Role
    {
        public Role() 
        {
            TheRole = Roles.User;
            Id = (int)TheRole;
        }

        public int Id { get; set; }
        public Roles TheRole { get; set; }
        public ICollection<User> Users { get; set;} = new List<User>();

    }
}
