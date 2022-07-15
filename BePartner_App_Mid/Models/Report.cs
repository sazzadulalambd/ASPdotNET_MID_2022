using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class Report
    {
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*Invalid Email")]
        [Required(ErrorMessage = "*Enter Email Address")]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "*Subject required")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "*Write some Messege")]
        [StringLength(500, MinimumLength = 25, ErrorMessage = "*Must be more than 25 characters.")]
        public string WMFH { get; set; }


    }
}