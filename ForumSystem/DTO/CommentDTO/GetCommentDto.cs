﻿using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO.CommentDTO
{
    public class GetCommentDto
    {
        [MinLength(32, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(8192, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Content { get; set; }

        public int Likes { get; set; }

        // needed to perform security checks
        //Може да се махне когато се премести мапването от сървиса в контролера.
        //Ще трябва да се мапне само името на юзъра а не целия обект при връщане - както е в GetCommentDtoNew.
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
