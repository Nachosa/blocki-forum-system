﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.UserViewModels
{
    public class EditUser
    {
        public int Id { get; set; }
        [MinLength(4, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string FirstName { get; set; }


        [MinLength(4, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string LastName { get; set; }



        [EmailAddress(ErrorMessage = "Invalid {0}!")]
        public string Email { get; set; }


        public string Password { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        public IFormFile ProfilePic { get; set; }
        public string ProfilePicPath { get; set; }

        public string DeleteProfilePicOption { get; set; }
    }
}
