﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.Models
{
    public class Tag
    {
        
        // [Key] -> creates a primary key column for 'Id' property
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        // can be null
        public DateTime? DeletedOn { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
       
    }
}
