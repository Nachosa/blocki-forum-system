using System.ComponentModel.DataAnnotations;

namespace ForumSystem.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int PostId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
