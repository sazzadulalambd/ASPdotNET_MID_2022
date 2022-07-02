using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class InvestorSend
    {
        
        public string Sender { get; set; }
        [Required]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "*Empty message")]
        public string Message { get; set; }

        public string time { get; set; }
        public string Status { get; set; }

    }
}