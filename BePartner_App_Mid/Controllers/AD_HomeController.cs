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

                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();


               // var db = new bePartnerCentralDatabaseEntities2();
                var ads = db.Admins.ToList();
                ViewBag.Alladmin = ads.Count();
                var ins = (from I in db.Investors where I.Status.Equals("Valid") select I).ToList();
                ViewBag.allInvaestors=ins.Count();
                var ens = (from I in db.Entrepreneurs where I.Status.Equals("Valid") select I).ToList();
                ViewBag.allEntrepreneurs=ens.Count();
                var ems = (from I in db.Employees where I.Status.Equals("Valid") select I).ToList();
                ViewBag.allEmployees=ems.Count();

                ViewBag.AD_Email = Session["Email"].ToString();
                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }

        [HttpPost]
        public ActionResult Delete_Account()
        {
            var email = Session["Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();
           
            db.Admins.Remove(Ed);
            Session.Contents.RemoveAll();
            ViewBag.dltMessage = "Account deleted";
            db.SaveChanges();
            return RedirectToAction("login_S", "S_Admin");
            
        }

        // make Employees valid 
        public ActionResult EmployeesValid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Employees where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status ="Valid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("All_Employee");
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
                return RedirectToAction("All_Employee");
            }
            catch
            {
                return View();
            }

        }


        // make invastor valid 
        public ActionResult InvestorsValid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Investors where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Valid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("All_Investor");
            }
            catch
            {
                return View();
            }

        }


        // make invastor INvalid 
        public ActionResult InvestorsInvalid(String id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Investors where I.Nid.Equals(id) select I).FirstOrDefault();

            Ed.Status = "Invalid";

            try
            {
                db.SaveChanges();
                return RedirectToAction("All_Investor");
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
                return RedirectToAction("All_Entrepreneur");
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
                return RedirectToAction("All_Entrepreneur");
            }
            catch
            {
                return View();
            }

        }


        [HttpGet]
        public ActionResult Account()
        {
            if (Session["Email"] != null)
            {
                
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var db = new bePartnerCentralDatabaseEntities2();
                var AD = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();
                
                //ViewBag.AD_Img = Session["Admin_img"].ToString();
                ViewBag.AD_Email = email;
                return View(AD);


            }
            return RedirectToAction("login_S", "S_Admin");

            //return View();
        }
        [HttpPost]
        public ActionResult Account(Admin_AccountEdit AAE)
        {
            if (ModelState.IsValid)
            {
                if (Admin_Account_Edit(AAE))
                {
                    ViewBag.Message = "Data Updated";
                    return RedirectToAction("Account");
                }

            }
            ViewBag.Message = "Data Update Failed";
            return View();
        }


        public bool Admin_Account_Edit(Admin_AccountEdit AAE)
        {
            var email = Session["Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Admins where I.Ad_Email.Equals(email) select I).FirstOrDefault();
            Ed.FirstName = AAE.FirstName;
            Ed.LastName = AAE.LastName;
            Ed.Gender = AAE.Gender;
            Ed.Dob = AAE.Dob;
            Ed.Phone = AAE.Phone;
            Ed.Nid = AAE.Nid;
            Ed.Ad_Email= email;
            Ed.Address = AAE.Address;

          
            if (AAE.Img != null)
            {
                string path = Server.MapPath("~/Img/ADMIN");
                string fileName = Path.GetFileName(AAE.Img.FileName);
                string fullpath = Path.Combine(path, fileName);
                AAE.Img.SaveAs(fullpath);  //save into img folder


                // for data base
                string databaseImgpath = "~/Img/ADMIN/" + fileName;
                Ed.Img = databaseImgpath;

            }
            else
            {
                Ed.Img = Ed.Img;
            }
            

            try
            {
                db.SaveChanges();
                
                Session["Email"] = AAE.Ad_Email;
                return true;
            }
            catch
            {
                return false;
            }
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
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var db = new bePartnerCentralDatabaseEntities2();
                var ads = db.Admins.ToList();

                ViewBag.AD_Email = Session["Email"].ToString();

                return View(ads);


            }
            return RedirectToAction("login_S", "S_Admin");
        }

        // Export All admin
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


        // Export valid Investor
        [HttpPost]
        public FileResult Export_all_valid_Investors()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[9]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Organization"),
               new DataColumn("TIN"),
               new DataColumn("Website"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from I in db.Investors where I.Status.Equals("Valid") select I).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.In_Email,
                    EAA.Phone,
                    EAA.Nid,
                    EAA.OrgName,
                    EAA.Tin,
                    EAA.OrgSite,
                    EAA.OrgLocation);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DT_A_A);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Active Investors.xlsx");
                }
            }

        }


        // Export new Investors
        [HttpPost]
        public FileResult Export_all_New_Investors()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[9]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Organization"),
               new DataColumn("TIN"),
               new DataColumn("Website"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from I in db.Investors where I.Status.Equals("Invalid") select I).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.In_Email,
                    EAA.Phone,
                    EAA.Nid,
                    EAA.OrgName,
                    EAA.Tin,
                    EAA.OrgSite,
                    EAA.OrgLocation);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DT_A_A);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "new Investors.xlsx");
                }
            }

        }




        // Export new Enterpreneur
        [HttpPost]
        public FileResult Export_all_New_Enterpreneurs()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[8]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Occupation"),
               new DataColumn("Gender"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from I in db.Entrepreneurs where I.Status.Equals("Invalid") select I).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.En_Email,
                    EAA.Phone,
                    EAA.Nid,
                    EAA.Occupation,
                    EAA.Gender,
                    EAA.Address);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DT_A_A);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "new Enterpreneur.xlsx");
                }
            }

        }



        // Export valid Enterpreneur
        [HttpPost]
        public FileResult Export_all_valid_Enterpreneurs()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[8]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Occupation"),
               new DataColumn("Gender"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from I in db.Entrepreneurs where I.Status.Equals("Valid") select I).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.En_Email,
                    EAA.Phone,
                    EAA.Nid,
                    EAA.Occupation,
                    EAA.Gender,
                    EAA.Address);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DT_A_A);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Active Enterpreneur.xlsx");
                }
            }

        }


        // Export valid Employees
        [HttpPost]
        public FileResult Export_all_valid_Employees()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[8]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Security key"),
               new DataColumn("Gender"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from I in db.Employees where I.Status.Equals("Valid") select I).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.Emp_Email,
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Active Employees.xlsx");
                }
            }

        }


        // Export New Employees
        [HttpPost]
        public FileResult Export_all_new_Employees()
        {
            DataTable DT_A_A = new DataTable("Grid");
            DT_A_A.Columns.AddRange(new DataColumn[8]
            {
               new DataColumn("First Name"),
               new DataColumn("Last Name"),
               new DataColumn("Emai"),
               new DataColumn("Phone"),
               new DataColumn("NID"),
               new DataColumn("Security key"),
               new DataColumn("Gender"),
               new DataColumn("Address")
            });
            var db = new bePartnerCentralDatabaseEntities2();
            var EAAS = (from I in db.Employees where I.Status.Equals("Invalid") select I).ToList();
            foreach (var EAA in EAAS)
            {
                DT_A_A.Rows.Add(
                    EAA.FirstName,
                    EAA.LastName,
                    EAA.Emp_Email,
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "New Employees.xlsx");
                }
            }

        }


        public ActionResult All_Employee()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var db = new bePartnerCentralDatabaseEntities2();
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

                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var db = new bePartnerCentralDatabaseEntities2();
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

                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var db = new bePartnerCentralDatabaseEntities2();
                var EN = (from I in db.Entrepreneurs where I.Status.Equals("Valid") select I).ToList();

                // var ens = db.Entrepreneurs.ToList();
                return View(EN);
            }
            return RedirectToAction("login_S", "S_Admin");
        }

        [HttpGet]
        public ActionResult AD_Notification()
        {
            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();

                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //ViewBag.AD_Email = Session["Email"].ToString();
               // var db = new bePartnerCentralDatabaseEntities2();
                List<admin_Notification> admin_nft  = new List<admin_Notification>();
  
                var nfts = new admin_Notification();

                var ems = (from I in db.Employees where I.Status.Equals("Invalid") select I).ToList();
                ViewBag.emss = ems.Count();
                foreach (var i_em in ems)
                {
                    nfts.Emp_FirstName = i_em.FirstName;
                    nfts.Emp_LastName = i_em.LastName;
                    nfts.Emp_Email = i_em.Emp_Email;
                    nfts.Emp_Phone = i_em.Phone;
                    nfts.Emp_Nid = i_em.Nid;
                    nfts.Emp_Address = i_em.Address;
                    nfts.Emp_Security_key=i_em.Security_key;
                    nfts.Emp_Gender = i_em.Gender;

                }

                var ins = (from I in db.Investors where I.Status.Equals("Invalid") select I).ToList();
                ViewBag.inss = ins.Count();
                foreach (var i_in in ins)
                {
                    nfts.In_FirstName = i_in.FirstName;
                    nfts.In_LastName = i_in.LastName;
                    nfts.In_Email = i_in.In_Email;
                    nfts.In_Phone = i_in.Phone;
                    nfts.In_Nid = i_in.Nid;
                    nfts.In_OrgName = i_in.OrgName;
                    nfts.In_OrgEmail = i_in.OrgEmail;
                    nfts.In_OrgLocation = i_in.OrgLocation;
                    nfts.In_Tin = i_in.Tin;
                    nfts.In_OrgSite = i_in.OrgSite;
                    nfts.In_EmailValidation = i_in.EmailValidation;

                }

                var ens = (from I in db.Entrepreneurs where I.Status.Equals("Invalid") select I).ToList();
                ViewBag.enss = ens.Count();
                foreach (var i_en in ens)
                { 
                    nfts.En_FirstName = i_en.FirstName;
                    nfts.En_LastName = i_en.LastName;
                    nfts.En_Email = i_en.En_Email;
                    nfts.En_Gender = i_en.Gender;
                    nfts.En_Address = i_en.Address;
                    nfts.En_Phone = i_en.Phone;
                    nfts.En_Nid = i_en.Nid; 
                    nfts.En_Occupation = i_en.Occupation;
                    admin_nft.Add(nfts);
                }


                    

                return View(admin_nft);

            }

            return RedirectToAction("login_S", "S_Admin");
        }


        [HttpGet]
        public ActionResult AD_Settings()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

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


        [HttpGet]
        public ActionResult AD_Messege()
        {

            if (Session["Email"] != null)
            {
                
                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var email = Session["Email"].ToString();
                //var db = new bePartnerCentralDatabaseEntities2();
                var reps = (from I in db.Reports where I.sender.Equals(email) select I).ToList();

                //var ins = db.Investors.ToList();
                return View(reps);
            }
            return RedirectToAction("login_S", "S_Admin");
        }

        [HttpPost]
        public ActionResult AD_Messege(Models.Report AR)
        {
            if (ModelState.IsValid)
            {
                var db = new bePartnerCentralDatabaseEntities2();
                var email = Session["Email"].ToString();
                var AD_report = new EF.Report()
                {
                    sender = email,
                    Receiver = AR.Receiver,
                    Title = AR.Subject,
                    Description = AR.WMFH,
                    Report_Time = DateTime.Now,
                    Status = "Sent"
                };

                db.Reports.Add(AD_report);

                db.SaveChanges();

                
            }
                return RedirectToAction("AD_Messege");
            
        }

            public ActionResult AD_Messege_R()
        {

            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                //new_messege
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                //var ins = db.Investors.ToList();
                return View(rep);
                
            }
            return RedirectToAction("login_S", "S_Admin");
        }


        public ActionResult AD_Chart()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }
        [HttpGet]
        public ActionResult AD_Task()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                return View();
            }
            return RedirectToAction("login_S", "S_Admin");
        }

        [HttpPost]
        public ActionResult AD_Task(Admin_Task AT)
        {
            if (ModelState.IsValid)
            {
                var db = new bePartnerCentralDatabaseEntities2();
                var email = Session["Email"].ToString();
                var AD_Task_ad = new Task()
                {
                    Subject = AT.Subject,
                    Task_Description = AT.WMFH,
                    Due_time = AT.datetime.Date,
                    Issue_time = DateTime.Now,
                    Status = "sent"
                };

                db.Tasks.Add(AD_Task_ad);

                db.SaveChanges();

            }

            return View();
        }

            public ActionResult AD_Task_panding()
        {


            if (Session["Email"] != null)
            {
                ViewBag.AD_Email = Session["Email"].ToString();

                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                //new_messege
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                var PT = (from I in db.Tasks where I.Status.Equals("sent") orderby I.TaskId select I).ToList();
                return View(PT);

            }
            return RedirectToAction("login_S", "S_Admin");
        }

       

        public ActionResult AD_Task_Complete()
        {

            if (Session["Email"] != null)
            {

                ViewBag.AD_Email = Session["Email"].ToString();
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

                var PT = (from I in db.Tasks where I.Status.Equals("Done") orderby I.TaskId select I).ToList();
                return View(PT);
            }
            return RedirectToAction("login_S", "S_Admin");
        }


        public ActionResult _AD_Layout()
        {

            if (Session["Email"] != null)
            {
                //new_messege
                var email = Session["Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var rep = (from I in db.Reports where I.Receiver.Equals(email) select I).ToList();
                Session["Totalmsg"] = rep.Count();
                ViewBag.Totalmsg = Session["Totalmsg"].ToString();

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
