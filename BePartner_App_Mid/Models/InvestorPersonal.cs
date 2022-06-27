using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class InvestorPersonal
    {
        [Required(ErrorMessage = "*Name required")]
        [RegularExpression("^[A-Za-z., ]*$", ErrorMessage = "*Invalid Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Name required")]
        [RegularExpression("^[A-Za-z., ]*$", ErrorMessage = "*Invalid Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="*Gender required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*Date of birth required")]
        [CustomValidation.CustomValid]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "*Address required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*Phone required")]
        [RegularExpression("[0]{1}[1]{1}[7,9,6,5,3]{1}[0-9]{8}", ErrorMessage = "*Invalid Number")]
        public string Phone { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*Invalid Email")]
        [Required(ErrorMessage = "*Email required")]
        [CustomValidation.InEmailCheck]
        public string In_Email { get; set; }

        [Required(ErrorMessage = "*NID required")]
        [RegularExpression("^([0-9]{10}|[0-9]{13})$", ErrorMessage = "*Invalid NID Number")]
        public string Nid { get; set; }

    }
}