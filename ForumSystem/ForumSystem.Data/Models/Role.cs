using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
   
    public class Role
    {
     

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set;} = new List<User>();

    }
}
