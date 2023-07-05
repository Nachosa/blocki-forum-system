using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemDTO.ViewModels.AdminModels
{
    public class SearchUser
    {
        public string SearchOption { get; set; }

        [Required(ErrorMessage = "Please enter Value!")]
        public string SearchOptionValue { get; set; }

        public List<User> Users { get; set; }= new List<User>();


    }
}
