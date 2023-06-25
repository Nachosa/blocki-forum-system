﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.TagDTO
{
    public class TagDto
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [MinLength(3, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Name { get; set; }
    }
}
