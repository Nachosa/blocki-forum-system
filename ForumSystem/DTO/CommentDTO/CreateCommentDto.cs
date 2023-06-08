﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CommentDTO
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "The {0} field is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [MinLength(32, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(8192, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Content { get; set; }
    }
}
