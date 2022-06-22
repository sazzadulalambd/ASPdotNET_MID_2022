using BePartner_App_Mid.EF;
using BePartner_App_Mid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BePartner_App_Mid.Controllers
{
    public class InvestorController : Controller
    {

        //InvestorEntity InRegData = new InvestorEntity();

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
                var In = new InvestorEntity();
                In.FirstName = Session["FirstName"].ToString();
                In.LastName = Session["LastName"].ToString();
                In.Dob = DateTime.Parse(Session["Dob"].ToString());
                In.Address = Session["Address"].ToString();
                In.Phone = Session["Phone"].ToString();
                In.Email = Session["Email"].ToString();
                In.Nid = Session["Nid"].ToString();
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
        public ActionResult Registration(InvestorEntity In)
        {
            //TempData["Investor"] = In;
            if (ModelState.IsValid)
            {
                Session["FirstName"] = In.FirstName;
                Session["LastName"] = In.LastName;
                Session["Dob"] = In.Dob;
                Session["Address"] = In.Address;
                Session["Phone"] = In.Phone;
                Session["Email"] = In.Email;
                Session["Nid"] = In.Nid;
                //ViewBag.Message = "Data Inserted!!";
                //if (this.RegistrationCreate(In))
                //{
                //    ViewBag.Message = "Data Inserted!!";
                //}
                //else
                //{
                //    ViewBag.Message = "Something went wrong!!";
                //}
                return RedirectToAction("Registration2");

            }
            else
            {
                ViewBag.Message = "Not Valid!!";
                
            }

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
        public ActionResult Registration2(InvestorEntity In)
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
        public ActionResult Registration3(InvestorEntity In)
        {
            if (ModelState.IsValid)
            {
                In.FirstName = Session["FirstName"].ToString();
                In.LastName = Session["LastName"].ToString();
                In.Dob = DateTime.Parse(Session["Dob"].ToString());
                In.Address = Session["Address"].ToString();
                In.Phone = Session["Phone"].ToString();
                In.Email = Session["Email"].ToString();
                In.Nid = Session["Nid"].ToString();

                
                In.OrgName = Session["OrgName"].ToString();
                In.OrgEstablished = DateTime.Parse(Session["OrgEstablished"].ToString());
                In.OrgLocation = Session["OrgLocation"].ToString();
                In.OrgEmail = Session["OrgEmail"].ToString();
                In.OrgPhone = Session["OrgPhone"].ToString();
                In.OrgLicense = Session["OrgLicense"].ToString();
                In.Tin = Session["Tin"].ToString();
                In.OrgSite = Session["OrgSite"].ToString();


                if (this.RegistrationCreate(In))
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

        public bool RegistrationCreate(InvestorEntity In)
        {
            var investor = new in_Investors()
            {
                FirstName = In.FirstName,
                LastName = In.LastName,
                Dob = In.Dob,
                Address = In.Address,
                Phone = In.Phone,
                Email = In.Email,
                Nid = In.Nid,

                OrgName = In.OrgName,
                OrgEstablished = In.OrgEstablished,
                OrgLocation = In.OrgLocation,
                OrgEmail = In.OrgEmail,
                OrgPhone = In.OrgPhone,
                OrgLicense = In.OrgLicense,
                Tin = In.Tin,
                OrgSite = In.OrgSite,

                Password = In.Password
            };
            var db = new bePartnerCentralDatabaseEntities();
            db.in_Investors.Add(investor);
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
        public ActionResult InLogin(InvestorEntity In)
        {
            var db = new bePartnerCentralDatabaseEntities();
            var investor = (from I in db.in_Investors where I.Email == In.Email select I).FirstOrDefault();
            if(investor != null)
            {
                if (investor.Password == In.Password)
                {
                    ViewBag.Message = "All Correct";
                }
                ViewBag.Message = "Incorrect Password. Not "+investor.FirstName+"?<br>Re-enter Email";
            }
            else
            {
                ViewBag.Message = "Incorrect Email";
            }
            return View();
        }
    }
}