using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ForumSystem.DataAccess.Models
{
    public class Comment : Entity
    {
        [Key]
        public int Id { get; set; }

        //[DataType(DataType.Text)]
        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        //[JsonIgnore]
        public User User { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        //[JsonIgnore]
        public Post Post { get; set; }

        //[JsonIgnore]
        public ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
