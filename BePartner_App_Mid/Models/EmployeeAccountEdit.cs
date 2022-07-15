using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class EmployeeAccountEdit
    {
        [Required(ErrorMessage = "*First Name required")]
        [RegularExpression("^[A-Za-z., ]*$", ErrorMessage = "*Invalid First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name required")]
        [RegularExpression("^[A-Za-z., ]*$", ErrorMessage = "*Invalid Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*Date of birth required")]
        [CustomValidation.CustomValid]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "*Address required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*Phone required")]
        [RegularExpression("[0]{1}[1]{1}[7,9,6,5,3,4]{1}[0-9]{8}", ErrorMessage = "*Invalid Number")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "*Security key")]
        public string Security_key { get; set; }


        [Required(ErrorMessage = "*Password required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "*Must be more than 8 characters.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "*NID required")]
        [RegularExpression("^([0-9]{10}|[0-9]{13})$", ErrorMessage = "*Invalid NID Number")]
        public string Nid { get; set; }
    }
}