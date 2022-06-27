using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class InvestorEditPass
    {
        [Required(ErrorMessage ="*Old password required")]
        [CustomValidation.InCheckPass]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*Password required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "*Must be more than 8 characters.")]
        public string NewPassword { get; set; }

        //[Required(ErrorMessage = "*Password required")]
        [Compare("NewPassword", ErrorMessage = "*Password not matching")]
        public string ConfirmNewPassword { get; set; }
    }
}