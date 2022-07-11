﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class Admin_AccountEdit
    {

        [Required(ErrorMessage = "*First Name")]
        [RegularExpression("^[A-Za-z., ]*$", ErrorMessage = "*Invalid First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name")]
        [RegularExpression("^[A-Za-z., ]*$", ErrorMessage = "*Invalid Last Name")]
        public string LastName { get; set; }

        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*Invalid Email")]
        //[Required(ErrorMessage = "*Enter Email Address")]
        //[CustomValidation.Admin_EV]
        public string Ad_Email { get; set; }


        [Required(ErrorMessage = "*Enter Phone number")]
        [RegularExpression("[0]{1}[1]{1}[7,9,6,5,3,4]{1}[0-9]{8}", ErrorMessage = "*Invalid Number")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "*Gender required")]
        public string Gender { get; set; }


        //[Required(ErrorMessage = "*Enter NID number")]
        [RegularExpression("^([0-9]{10}||[0-9]{13})", ErrorMessage = "*Invalid NID Number (10/13 digit)")]
        public string Nid { get; set; }


        [Required(ErrorMessage = "*Address required")]
        public string Address { get; set; }


        [Required(ErrorMessage = "*Date of birth")]
        [CustomValidation.CustomValid]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "*Security key")]
        public string Security_key { get; set; }

        public HttpPostedFileBase Img { get; set; }

    }
}