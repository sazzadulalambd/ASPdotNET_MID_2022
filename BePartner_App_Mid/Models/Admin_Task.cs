using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class Admin_Task
    {
        [Required(ErrorMessage = "*Invalid date")]
        [CustomValidation.Admin_DD]
        public DateTime datetime { get; set; }


        [Required(ErrorMessage = "*Subject required")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "*Write some Messege")]
        [StringLength(500, MinimumLength = 25, ErrorMessage = "*Must be more than 25 characters.")]
        public string WMFH { get; set; }

    }
}