using BePartner_App_Mid.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.CustomValidation
{
    public class InCheckPass:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = HttpContext.Current.Session["In_Email"].ToString();
                string message = value.ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var investor = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
                if(investor != null)
                {
                    if (investor.Password.Equals(message))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("*Password didn't match");
                }
            }
            return new ValidationResult("*Password didn't match");
        }
    }
}