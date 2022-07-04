using BePartner_App_Mid.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BePartner_App_Mid.Controllers
{
    public class J_EmployeeController : Controller
    {
        // GET: J_Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountInformation()
        {
            if (Session["Email"] != null)
            {
               // var email = Session["Email"].ToString();
              //  var db = new bePartnerCentralDatabaseEntities2();
               // var EM = (from I in db.Employee where I.Em_Email.Equals(email) select I).FirstOrDefault();
               // Session["Employee_img"] = EM.Img;

               // ViewBag.EM_Img = Session["Employee_img"].ToString();
               // ViewBag.EM_Email = email;
              //  return View(EM);


            }
            return RedirectToAction("login_S", "S_Admin");

            //return View();
        }
        public ActionResult EmployeeInformation()
        {
            return View();
        }
        public ActionResult InvestorInformation()
        {
            return View();
        }
        public ActionResult EnterprenurInformation()
        {
            return View();
        }
        public ActionResult TaskInformation()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
    }
}