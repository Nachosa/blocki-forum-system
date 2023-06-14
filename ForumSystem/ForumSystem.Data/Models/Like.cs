using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
    public class Like : Entity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public int? PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }

        public int? CommentId { get; set; }
        [JsonIgnore]
        public Comment? Comment { get; set; }
    }
}
