using System.ComponentModel.DataAnnotations;

namespace ForumSystem.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int PostId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
