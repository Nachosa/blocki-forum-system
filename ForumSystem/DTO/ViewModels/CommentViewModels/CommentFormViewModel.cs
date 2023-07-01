using ForumSystem.DataAccess.Models;

namespace ForumSystemDTO.ViewModels.CommentViewModels
{
    public class CommentFormViewModel
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        public string CommentContent { get; set; }
    }
}