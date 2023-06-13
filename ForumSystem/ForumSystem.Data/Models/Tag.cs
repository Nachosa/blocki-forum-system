using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
    public class Tag : Entity
    {
        
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PostTag> Posts { get; set; } = new List<PostTag>();

        #region methods
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Tag other = (Tag)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
