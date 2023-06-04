using ForumSystemBusiness.Models;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models
{
    public class User : Person
    {
        public bool Restricted { get; set; }
    }
}
