using BePartner_App_Mid.EF;
using BePartner_App_Mid.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }




        public ActionResult Account()
        {
            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var AD = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();
                Session["Admin_img"] = AD.Img;

                ViewBag.AD_Img = Session["Admin_img"].ToString();
                ViewBag.AD_Email = email;
                return View(AD);


            }
            return RedirectToAction("login_S", "S_Admin");

            //return View();
        }



        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("login_S", "S_Admin");
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


        [HttpPost]
        public FileResult Export_all_Active_admin()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[8]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Security_key"),
               new DataColumn("Gender"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from EAA in db.Admins.ToList() select EAA).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.Ad_Email,
                    EAA.Phone,
                    EAA.Nid,
                    EAA.Security_key,
                    EAA.Gender,
                    EAA.Address);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DT_A_A);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "All_Admin.xlsx");
                }
            }



        }


        public ActionResult All_Employee()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ems = (from I in db.Employees where I.Status.Equals("Valid") select I).ToList();
                //var ems = db.Employees.ToList();
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
                var ins = (from I in db.Investors where I.Status.Equals("Valid") select I).ToList();

                //var ins = db.Investors.ToList();
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
                var EN = (from I in db.Entrepreneurs where I.Status.Equals("Valid") select I).ToList();

                // var ens = db.Entrepreneurs.ToList();
                return View(EN);
            }
            return RedirectToAction("login_S", "S_Admin");
        }


        public ActionResult AD_Notification()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ins = (from I in db.Investors where I.Status.Equals("Invalid") select I).ToList();
                //var ins = db.Investors.ToList();
                return View(ins);

                //var ens = db.Entrepreneurs.ToList();
                // return View(ens);

            }

            return RedirectToAction("login_S", "S_Admin");
        }


        [HttpGet]
        public ActionResult AD_Settings()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }

        [HttpPost]
        public ActionResult AD_Settings(Admin_PasswordEdit APE)
        {

            if (ModelState.IsValid)
            {
                if (Admin_Update_Password(APE))
                {
                    ViewBag.APM_G = "Password Updated";
                    //return RedirectToAction("Account");
                    return View();
                }
            }
            ViewBag.APM_R = "Password Update Failed";
            return View();
        }



        public ActionResult AD_Messege()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }


        public ActionResult AD_Chart()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }


        public ActionResult _AD_Layout()
        {

            if (Session["Email"] != null)
            {

                // ViewBag.AD_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("login_S", "S_Admin");

        }




        public bool Admin_Update_Password(Admin_PasswordEdit APE_get)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var email = Session["Email"].ToString();
            var AD_pass = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();

            AD_pass.Password = APE_get.ConfirmPassword;

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