using BePartner_App_Mid.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.CustomValidation
{
    public class Admin_EV : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

                if (HttpContext.Current.Session["Email"] != null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    string message = value.ToString();
                    var db = new bePartnerCentralDatabaseEntities2();
                    var Admin = (from I in db.Admins where I.Ad_Email.Equals(message) select I).FirstOrDefault();
                    if (Admin != null)
                    {
                        return new ValidationResult("*Email already exists");
                    }
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("*Something went wrong");
        }
    }
}
