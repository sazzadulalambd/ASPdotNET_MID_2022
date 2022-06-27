using BePartner_App_Mid.EF;
using BePartner_App_Mid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BePartner_App_Mid.Controllers
{
    [Authorize]
    public class InHomeController : Controller
    {
        public ActionResult InProfile()
        {
            var email = Session["In_Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var In = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();

            return View(In);
        }

        [HttpGet]
        public ActionResult InEditPersonal()
        {
            var email = Session["In_Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var In = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
            return View(In);
        }

        [HttpPost]
        public ActionResult InEditPersonal(InvestorPersonal In)
        {
            if (ModelState.IsValid)
            {
                if (InUpdatePer(In))
                {
                    ViewBag.Message = "Data Updated";
                    return RedirectToAction("InProfile");
                }

            }
            ViewBag.Message = "Data Update Failed";
            return View(In);

        }

        [HttpGet]
        public ActionResult InEditProfessional()
        {
            var email = Session["In_Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var In = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
            return View(In);
        }

        [HttpPost]
        public ActionResult InEditProfessional(InvestorProfational In)
        {
            if (ModelState.IsValid)
            {
                if (InUpdatePro(In))
                {
                    ViewBag.Message = "Data Updated";
                    return RedirectToAction("InProfile");
                }
            }
            var db = new bePartnerCentralDatabaseEntities2(); 
            var email = Session["In_Email"].ToString();
            var Ed = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
            ViewBag.Message = "Data Update Failed";
            return View(Ed);
        }

        [HttpGet]
        public ActionResult InEditPass()
        {
            //var email = Session["In_Email"].ToString();
            //var db = new bePartnerCentralDatabaseEntities2();
            //var In = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult InEditPass(InvestorEditPass In)
        {
            if (ModelState.IsValid)
            {
                if (InUpdatePass(In))
                {
                    ViewBag.Message = "Data Updated";
                    return RedirectToAction("InProfile");
                }
            }
            ViewBag.Message = "Data Update Failed";
            return View();
        }


        public bool InUpdatePer(InvestorPersonal In)
        {
            var email = Session["In_Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
            Ed.FirstName = In.FirstName;
            Ed.LastName = In.LastName;
            Ed.Gender = In.Gender;
            Ed.Dob = In.Dob;
            Ed.Phone = In.Phone;
            Ed.Nid = In.Nid;
            Ed.Address = In.Address;
            //Ed.In_Email = In.In_Email;

            try
            {
                db.SaveChanges();
                //Session.RemoveAll();
                //Session["In_Email"] = In.In_Email;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InUpdatePro(InvestorProfational In)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var email = Session["In_Email"].ToString();
            var Ed = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();

            Ed.OrgName = In.OrgName;
            Ed.OrgEstablished = In.OrgEstablished;
            Ed.OrgLocation = In.OrgLocation;
            Ed.OrgEmail = In.OrgEmail;
            Ed.OrgPhone = In.OrgPhone;
            Ed.OrgLicense = In.OrgLicense;
            Ed.Tin = In.Tin;
            Ed.OrgSite = In.OrgSite;

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

        public bool InUpdatePass(InvestorEditPass In)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var email = Session["In_Email"].ToString();
            var Ed = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();

            Ed.Password = In.ConfirmNewPassword;

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
    }
}