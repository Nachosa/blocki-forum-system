using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
    public class PostTag
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }


        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
