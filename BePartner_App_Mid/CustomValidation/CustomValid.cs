using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.CustomValidation
{
    public class CustomValid:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                string message = value.ToString();
                //DateTime date = DateTime.Parse(message);
                DateTime dob = DateTime.Parse(message, CultureInfo.InvariantCulture);
                DateTime today = DateTime.Now;
                int year = today.Year - dob.Year;
                int month = today.Month - dob.Month;
                if (month < 0 && year > 0)
                {
                    year--;
                    month = Math.Abs(month);
                }
                if (year < 18)
                {
                    return new ValidationResult("Invalid Age: " + year.ToString() + " year(s) " + month.ToString() + " month(s).");
                }

            }

            return ValidationResult.Success;

        }
    }
}