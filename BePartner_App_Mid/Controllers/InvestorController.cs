using BePartner_App_Mid.EF;
using BePartner_App_Mid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BePartner_App_Mid.Controllers
{
    public class InvestorController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        // GET: Investor
        [HttpGet]
        public ActionResult Registration()
        {
            if (Session["FirstName"] is string)
            {
                var In = new InvestorPersonal();
                In.FirstName = Session["FirstName"].ToString();
                In.LastName = Session["LastName"].ToString();
                In.Dob = DateTime.Parse(Session["Dob"].ToString());
                In.Address = Session["Address"].ToString();
                In.Phone = Session["Phone"].ToString();
                In.In_Email = Session["Email"].ToString();
                In.Nid = Session["Nid"].ToString();
                In.Gender = Session["Gender"].ToString();
                ViewBag.Message = "data";
                return View(In); 
            }
            else
            {
                ViewBag.Message = "No Data";
                return View();
            }
            //return View();

        }

        [HttpPost]
        public ActionResult Registration(InvestorPersonal In)
        {
            //TempData["Investor"] = In;
            if (ModelState.IsValid)
            {
                Session["FirstName"] = In.FirstName;
                Session["LastName"] = In.LastName;
                Session["Dob"] = In.Dob.Date;
                Session["Address"] = In.Address;
                Session["Phone"] = In.Phone;
                Session["Email"] = In.In_Email;
                Session["Nid"] = In.Nid;
                Session["Gender"] = In.Gender;
                
                return RedirectToAction("Registration2");

            }
            else
            {
                ViewBag.Message = "Not Valid!!";
                
            }

            ViewBag.Message = In.Gender;
            return View(In);
           
            //return RedirectToAction("Registration2");
            //Registration2(In);
        }

        [HttpGet]
        public ActionResult Registration2()
        {
            
            
            return View();
        }

        [HttpPost]
        public ActionResult Registration2(InvestorProfational In)
        {
            //ViewBag.Message = Session["FirstName"].ToString();
            if (ModelState.IsValid)
            {
                Session["OrgName"] = In.OrgName;
                Session["OrgEstablished"] = In.OrgEstablished.ToString();
                Session["OrgLocation"] = In.OrgLocation;
                Session["OrgEmail"] = In.OrgEmail;
                Session["OrgPhone"] = In.OrgPhone;
                Session["OrgLicense"] = In.OrgLicense;
                Session["Tin"] = In.Tin;
                Session["OrgSite"] = In.OrgSite;

                return RedirectToAction("Registration3");
            }
            else
            {
                ViewBag.Message = "not valid";
            }
            //ViewBag.Message = In.FirstName;
            return View(In);
        }

        [HttpGet]
        public ActionResult Registration3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration3(InvestorPassword Pass)
        {
            if (ModelState.IsValid)
            {
                var Par = new InvestorPersonal();
                Par.FirstName = Session["FirstName"].ToString();
                Par.LastName = Session["LastName"].ToString();
                Par.Dob = DateTime.Parse(Session["Dob"].ToString());
                Par.Address = Session["Address"].ToString();
                Par.Phone = Session["Phone"].ToString();
                Par.In_Email = Session["Email"].ToString();
                Par.Nid = Session["Nid"].ToString();
                Par.Gender = Session["Gender"].ToString();

                var Pro = new InvestorProfational(); 
                Pro.OrgName = Session["OrgName"].ToString();
                Pro.OrgEstablished = DateTime.Parse(Session["OrgEstablished"].ToString());
                Pro.OrgLocation = Session["OrgLocation"].ToString();
                Pro.OrgEmail = Session["OrgEmail"].ToString();
                Pro.OrgPhone = Session["OrgPhone"].ToString();
                Pro.OrgLicense = Session["OrgLicense"].ToString();
                Pro.Tin = Session["Tin"].ToString();
                Pro.OrgSite = Session["OrgSite"].ToString();


                if (this.RegistrationCreate(Par, Pro, Pass))
                {
                    ViewBag.Message = "Data Inserted!!";
                }
                else
                {
                    ViewBag.Message = "Something went wrong!!";
                }
            }
            return View();
        }

        public bool RegistrationCreate(InvestorPersonal Par, InvestorProfational Pro, InvestorPassword Paa)
        {
            
            var investor = new Investor()
            {
                FirstName = Par.FirstName,
                LastName = Par.LastName,
                Dob = Par.Dob,
                Address = Par.Address,
                Phone = Par.Phone,
                In_Email = Par.In_Email,
                Nid = Par.Nid,
                Gender = Par.Gender,

                OrgName = Pro.OrgName,
                OrgEstablished = Pro.OrgEstablished,
                OrgLocation = Pro.OrgLocation,
                OrgEmail = Pro.OrgEmail,
                OrgPhone = Pro.OrgPhone,
                OrgLicense = Pro.OrgLicense,
                Tin = Pro.Tin,
                OrgSite = Pro.OrgSite,
                
                Password = Paa.Password,

                Img = null,
                EmailValidation = "No",
                Status = "Invalid"
                
            };
            var db = new bePartnerCentralDatabaseEntities2();
            db.Investors.Add(investor);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }



        [HttpGet]
        public ActionResult InLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InLogin(InvestorLogin In, string ReturnUrl)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var investor = (from Id in db.Investors where Id.In_Email.Equals(In.In_Email) select Id).FirstOrDefault();
            if(investor != null)
            {
                if (investor.Password.Equals(In.Password))
                {
                    FormsAuthentication.SetAuthCookie(In.In_Email, true);
                    Session.RemoveAll();
                    Session["In_Email"] = In.In_Email;
                    HttpCookie c1 = new HttpCookie("InInfo");
                    c1.Value = investor.In_Email.ToString();
                    c1.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(c1);
                    if (ReturnUrl != null)
                    {
                        string[] arr = ReturnUrl.Split('/');
                        ViewBag.Message = arr[0].ToString();
                        //return View(In);
                        return RedirectToAction(arr[2].ToString(), arr[1].ToString());
                    }
                    return RedirectToAction("InProfile","InHome");
                }
                ViewBag.Message = "Incorrect Password. Not "+investor.FirstName+"?<br>Re-enter Email";
            }
            else
            {
                ViewBag.Message = "Incorrect Email";
            }
            return View(In);
        }

        

    }
}