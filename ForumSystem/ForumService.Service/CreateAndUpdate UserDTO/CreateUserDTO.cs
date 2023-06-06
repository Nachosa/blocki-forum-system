﻿using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.CreateAndUpdate_UserDTO
{
    public class CreateUserDTO
    {

        [Required(ErrorMessage ="Please enter {0}!")] 
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

    }
}
