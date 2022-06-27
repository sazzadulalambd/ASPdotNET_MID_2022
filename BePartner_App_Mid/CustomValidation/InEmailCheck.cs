using BePartner_App_Mid.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.CustomValidation
{
    public class InEmailCheck:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                
                if(HttpContext.Current.Session["In_Email"] != null)
                {
                    //var email = HttpContext.Current.Session["In_Email"].ToString();
                    //string message = value.ToString();
                    //var db = new bePartnerCentralDatabaseEntities2();
                    //var investor = (from I in db.Investors where I.In_Email.Equals(message) select I).FirstOrDefault();
                    //if (investor != null)
                    //{
                    //    if (email.Equals(message))
                    //    {
                    //        return ValidationResult.Success;
                    //    }
                    //    return new ValidationResult("*Email already exists");
                    //}
                    return ValidationResult.Success;
                }
                else
                {
                    string message = value.ToString();
                    var db = new bePartnerCentralDatabaseEntities2();
                    var investor = (from I in db.Investors where I.In_Email.Equals(message) select I).FirstOrDefault();
                    if (investor != null)
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