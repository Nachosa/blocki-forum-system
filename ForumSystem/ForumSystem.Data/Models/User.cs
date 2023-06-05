using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Business.Models
{
    public class User : Person
    {
        public bool Restricted { get; set; }
    }
}
