using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClass.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name field is required")]
        [MaxLength(50, ErrorMessage = "The name field can not exceed 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The name field is required")]
        [MaxLength(50, ErrorMessage = "The name field can not exceed 50 characters")]
        public string Password { get; set; }
    }
}

