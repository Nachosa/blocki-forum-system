using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
    public class PostTag
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        //[JsonIgnore]
        public Post Post { get; set; }

        public int TagId { get; set; }
        //[JsonIgnore]
        public Tag Tag { get; set; }
    }
}
