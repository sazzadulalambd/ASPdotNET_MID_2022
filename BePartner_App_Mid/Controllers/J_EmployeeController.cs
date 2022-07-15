using BePartner_App_Mid.EF;
using BePartner_App_Mid.Models;
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
            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ems = (from I in db.Entrepreneurs where I.Status.Equals("Invalid") select I).ToList();
                ViewBag.x = ems.Count();
                return View(ems);
            }
            return RedirectToAction("SignIn", "Employee");
        }

        [HttpPost]
        public ActionResult Delete_Account()
        {
            var email = Session["Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Employees where I.Emp_Email.Equals(email) select I).FirstOrDefault();

            db.Employees.Remove(Ed);
            Session.Contents.RemoveAll();
            ViewBag.dltMessage = "Account deleted";
            db.SaveChanges();
            return RedirectToAction("SignIn", "Employee");

        }

        [HttpGet]
        public ActionResult AccountInformation()
        {
            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var em = (from I in db.Employees where I.Emp_Email.Equals(email) select I).FirstOrDefault();

                ViewBag.EM_Email = email;
                return View(em);


            }
            return RedirectToAction("SignIn", "Employee");
        }

        [HttpPost]
        public ActionResult AccountInformation(EmployeeAccountEdit emreg)
        {
            if (ModelState.IsValid)
            {
                    var email = Session["Email"].ToString();
                    var db = new bePartnerCentralDatabaseEntities2();
                    var Ed = (from I in db.Employees where I.Emp_Email.Equals(email) select I).FirstOrDefault();
                    Ed.FirstName = emreg.FirstName;
                    Ed.LastName = emreg.LastName;
                    Ed.Address = emreg.Address;
                    Ed.Nid = emreg.Nid;
                    Ed.Phone = emreg.Phone;
                    Ed.Password = emreg.Password;
                    Ed.Dob=emreg.Dob;
                    Ed.Security_key=emreg.Security_key;

                db.SaveChanges();

                return View();
            }
            return View();
            //return RedirectToAction("AccountInformation");

        }

        public ActionResult AdminInformation()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                //var ems = (from I in db.Investors where I.Status.Equals("Valid") select I).ToList();
                var ems = db.Admins.ToList();
                ViewBag.x = ems.Count();
                return View(ems);


            }
            return RedirectToAction("SignIn", "Employee");
        }
        public ActionResult EmployeeInformation()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ems = (from I in db.Employees where I.Status.Equals("Valid") select I).ToList();
                ViewBag.x = ems.Count();
                //var ems = db.Employees.ToList();
                return View(ems);


            }
            return RedirectToAction("SignIn", "Employee");
        }
        public ActionResult InvestorInformation()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                //var ems = (from I in db.Investors where I.Status.Equals("Valid") select I).ToList();
                var ems = db.Investors.ToList();
                ViewBag.x = ems.Count();
                return View(ems);


            }
            return RedirectToAction("SignIn", "Employee");
        }
        public ActionResult EnterprenurInformation()
        {
            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var ems = (from I in db.Entrepreneurs where I.Status.Equals("Valid") select I).ToList();
                ViewBag.x =ems.Count();
                return View(ems);


            }
            return RedirectToAction("SignIn", "Employee");
        }
        public ActionResult TaskInformation()
        {
            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var PT = (from I in db.Tasks where I.Status.Equals("sent") orderby I.TaskId select I).ToList();
                return View(PT);
            }
            return RedirectToAction("SignIn", "Employee");
        }

        public ActionResult Task_Information_done()
        {
            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var db = new bePartnerCentralDatabaseEntities2();
                var PT = (from I in db.Tasks where I.Status.Equals("Done") orderby I.TaskId select I).ToList();
                return View(PT);
            }
            return RedirectToAction("SignIn", "Employee");
        }

        public ActionResult About()
        {
            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("SignIn", "Employee");
        }

        public ActionResult Blog()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("SignIn", "Employee");
        }

        public ActionResult Contact()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("SignIn", "Employee");
        }

        [HttpGet]
        public ActionResult Messege_add()
        {

            if (Session["Email"] != null)
            {
                return View();
            }
            return RedirectToAction("SignIn", "Employee");
        }
        [HttpPost]
        public ActionResult Messege_add(Models.Report AR)
        {
            if (ModelState.IsValid)
            {
                var db = new bePartnerCentralDatabaseEntities2();
                var email = Session["Email"].ToString();
                var EM_report = new EF.Report()
                {
                    sender = email,
                    Receiver = AR.Receiver,
                    Title = AR.Subject,
                    Description = AR.WMFH,
                    Report_Time = DateTime.Now,
                    Status = "Sent"
                };

                db.Reports.Add(EM_report);

                db.SaveChanges();


            }
            return RedirectToAction("Messege_sent");
        }


        public ActionResult Messege_sent()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();


                ViewBag.EM_Email = Session["Email"].ToString();

                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var reps = (from I in db.Reports where I.sender.Equals(email) select I).ToList();

                return View(reps);
                
            }
            return RedirectToAction("SignIn", "Employee");
        }

        public ActionResult Messege_received()
        {

            if (Session["Email"] != null)
            {
                ViewBag.EM_Email = Session["Email"].ToString();

                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                
                return View(rep);
               
            }
            return RedirectToAction("SignIn", "Employee");
        }


        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("SignIn","Employee");
        }



        // make Employees valid 
        public ActionResult EmployeesValid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Employees where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Valid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("EmployeeInformation");
            }
            catch
            {
                return View();
            }

        }


        // make Employees INvalid 
        public ActionResult EmployeesInvalid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Employees where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Invalid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("EmployeeInformation");
            }
            catch
            {
                return View();
            }

        }


        

        // make Entrepreneur valid 
        public ActionResult EntrepreneursValid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Entrepreneurs where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Valid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("EnterprenurInformation");
            }
            catch
            {
                return View();
            }

        }


        // make Entrepreneur INvalid 
        public ActionResult EntrepreneursInvalid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Entrepreneurs where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Invalid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("EnterprenurInformation");
            }
            catch
            {
                return View();
            }

        }

        // make task Complete
        public ActionResult Task_Complete(int id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Tasks where I.TaskId.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Done";

            try
            {
                db.SaveChanges();
                return RedirectToAction("TaskInformation");
            }
            catch
            {
                return View();
            }

        }
    }
}