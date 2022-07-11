using BePartner_App_Mid.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.CustomValidation
{
    public class Admin_PE : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = HttpContext.Current.Session["Email"].ToString();
                string message = value.ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var admin = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();
                if (admin != null)
                {
                    if (admin.Password.Equals(message))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("*Password didn't match");
                }
            }
            return new ValidationResult("*Enter Password");
        }
    }
}
