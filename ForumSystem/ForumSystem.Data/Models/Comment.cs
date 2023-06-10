using System.ComponentModel.DataAnnotations;

namespace ForumSystem.DataAccess.Models
{
    public class Comment : Entity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public ICollection<User> Likes { get; set; } = new List<User>();

    }
}
