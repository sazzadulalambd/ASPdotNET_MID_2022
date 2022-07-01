using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class S_Admin_Model_Reg02
    {

        [Required(ErrorMessage = "*Date of birth")]
        [CustomValidation.CustomValid]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "*Security key")]
        public string Security_key { get; set; }


        [Required(ErrorMessage = "*Password required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "*Must be more than 8 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Confirm Password required")]
        [Compare("Password", ErrorMessage = "*Password not matching")]
        public string ConfirmPassword { get; set; }


        public HttpPostedFileBase IMG_file { get; set; }

    }
}