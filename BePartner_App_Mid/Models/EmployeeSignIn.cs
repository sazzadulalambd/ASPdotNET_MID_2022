using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class EmployeeSignIn
    {

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*Invalid Email")]
        [Required(ErrorMessage = "*Email required")]
        [CustomValidation.InEmailCheck]
        public string Email { get; set; }
        [Required(ErrorMessage = "*Password required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "*Must be more than 8 characters.")]
        public string Password { get; set; }

    }
}