using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public User User { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }

        public ICollection<User> Likes { get; set; } = new List<User>();

    }
}
