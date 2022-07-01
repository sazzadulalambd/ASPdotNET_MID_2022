using BePartner_App_Mid.EF;
using BePartner_App_Mid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BePartner_App_Mid.Controllers
{
    public class AD_HomeController : Controller
    {
        // GET: AD_Home
        public ActionResult Index()
        {
            ViewBag.AD_Email = Session["Email"].ToString();
            return View();
        }

       

      
        public ActionResult Account()
        {
            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var AD = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();
                Session["Admin_img"] = AD.Img;

                ViewBag.AD_Img= Session["Admin_img"].ToString();
                ViewBag.AD_Email = email;
                return View(AD);
               

            }
            return RedirectToAction("login_S", "S_Admin");

            //return View();
        }



            public ActionResult Logout()
        {
            return View();
        }

        public ActionResult All_Admin()
        {
            if (Session["Email"] != null)
            {
                var db = new bePartnerCentralDatabaseEntities2();
                var ads = db.Admins.ToList();

                ViewBag.AD_Email = Session["Email"].ToString();

                return View(ads);
                
                
            }
            return RedirectToAction("login_S", "S_Admin");
        }
          

        public ActionResult All_Employee()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ems = db.Employees.ToList();
                return View(ems);

            }
            return RedirectToAction("login_S", "S_Admin");
        }
        public ActionResult All_Investor()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ins = db.Investors.ToList();
                return View(ins);
            }
            return RedirectToAction("login_S", "S_Admin");
        }
        public ActionResult All_Entrepreneur()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ens = db.Entrepreneurs.ToList();
                return View(ens);
            }
            return RedirectToAction("login_S", "S_Admin");
        }
        public ActionResult AD_Notification()
        {
            ViewBag.AD_Email = Session["Email"].ToString();

            return View();
        }
        public ActionResult AD_Settings()
        {
            ViewBag.AD_Email = Session["Email"].ToString();

            return View();
        }
        public ActionResult AD_Messege()
        {
            ViewBag.AD_Email = Session["Email"].ToString();

            return View();
        }

     
        public ActionResult _AD_Layout()
        {
          if (Session["Email"] != null)
            {
               
            }
            return RedirectToAction("login_S", "S_Admin");

        }



        
    }
}