using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BePartner_App_Mid.Models
{
    public class Admin_PasswordEdit
    {
        [Required(ErrorMessage = "*Old password required")]
        [CustomValidation.Admin_PE]
        public string Old_Password { get; set; }

        [Required(ErrorMessage = "*New Password required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "*Must be more than 8 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Confirm Password required")]
        [Compare("Password", ErrorMessage = "*Password not matching")]
        public string ConfirmPassword { get; set; }
    }
}