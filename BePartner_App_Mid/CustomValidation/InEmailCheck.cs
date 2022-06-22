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
                string message = value.ToString();
                var db = new bePartnerCentralDatabaseEntities();
                var investor = (from I in db.in_Investors where I.Email == message select I).FirstOrDefault();
                if(investor != null)
                {
                    return new ValidationResult("*Email already exixts");
                }
            }
            return ValidationResult.Success;
        }
    }
}