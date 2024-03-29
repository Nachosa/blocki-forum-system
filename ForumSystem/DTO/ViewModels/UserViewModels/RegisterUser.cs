﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemDTO.ViewModels.UserViewModels
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Please enter {0}!")]
        [MinLength(4, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter {0}!")]
        [MinLength(4, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(32, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter {0}!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter {0}!"), EmailAddress(ErrorMessage = "Invalid {0}!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter {0}")]
        public string Password { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        public IFormFile ProfilePic { get; set; }

        public string ProfilePicPath { get; set; }
    }
}
