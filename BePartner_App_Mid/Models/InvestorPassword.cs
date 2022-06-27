using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class InvestorPassword
    {
        [Required(ErrorMessage = "*Password required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "*Must be more than 8 characters.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "*Password required")]
        [Compare("Password", ErrorMessage = "*Password not matching")]
        public string ConfirmPassword { get; set; }
    }
}