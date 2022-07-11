using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.Security;
using BePartner_App_Mid.Models;
using BePartner_App_Mid.EF;
using static System.Net.WebRequestMethods;



namespace BePartner_App_Mid.Controllers
{
    public class S_AdminController : Controller
    {
        // GET: S_Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Regstration_S1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Regstration_S1(S_Admin_Model_Reg01 SAMR1)
        {
            if (ModelState.IsValid)
            {
                Session["FirstName"] = SAMR1.FirstName;
                Session["LastName"] = SAMR1.LastName;
                Session["Email"] = SAMR1.Email;
                Session["Phone"] = SAMR1.Phone;
                Session["Gender"] = SAMR1.Gender;
                Session["NID"] = SAMR1.NID;
                Session["Address"] = SAMR1.Address;
                
              
                return RedirectToAction("Regstration_S2");

            }
            else
            {
               // ViewBag.Message = "Not Valid!!";

            }
            return View();
        }

        [HttpGet]
        public ActionResult Regstration_S2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Regstration_S2(S_Admin_Model_Reg02 SAMR2)
        {
            if (ModelState.IsValid)
            {

                Session["DOB"] = SAMR2.DOB.ToString();
                Session["Security_key"] = SAMR2.Security_key;
                Session["Password"] = SAMR2.Password;
                Session["ConfirmPassword"] = SAMR2.ConfirmPassword;
                if (SAMR2.IMG_file != null)
                {
                    string path = Server.MapPath("~/Img/ADMIN");
                    string fileName = Path.GetFileName(SAMR2.IMG_file.FileName);
                    string fullpath = Path.Combine(path, fileName);
                    SAMR2.IMG_file.SaveAs(fullpath);  //save into img folder


                    // for data base
                    string databaseImgpath = "~/Img/ADMIN/" + fileName; 
                    Session["IMG_file"] = databaseImgpath;

                }

                


                var SAMR1 = new S_Admin_Model_Reg01();
                if (this.RegistrationCreate( SAMR1 , SAMR2))
                {
                    return RedirectToAction("login_S");


                    // ViewBag.Message = "Data Inserted!!";
                }
                else
                {
                    // ViewBag.Message = "Something went wrong!!";
                }
            }
            else
            {
                // ViewBag.Message = "not valid";
            }
            return View();
        }



        public bool RegistrationCreate(S_Admin_Model_Reg01 SAM1 , S_Admin_Model_Reg02 SAM2)
        {

            var admin = new Admin()
            {
                Ad_Email=Session["Email"].ToString(),
                FirstName=Session["FirstName"].ToString(),
                LastName=Session["LastName"].ToString(),
                Address=Session["Address"].ToString(),
                Phone=Session["Phone"].ToString(),
                Security_key=Session["Security_key"].ToString(),
                Gender=Session["Gender"].ToString(),
                Nid=Session["NID"].ToString(),
                Password=Session["Password"].ToString(),
                Img=Session["IMG_file"].ToString(),
                Dob = DateTime.Parse(Session["DOB"].ToString())

            };
            var db = new bePartnerCentralDatabaseEntities2();
            db.Admins.Add(admin);
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
        public ActionResult login_S()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login_S(S_Admin_model_login SAML, string ReturnUrl)
        {

            var db = new bePartnerCentralDatabaseEntities2();
            var ad = (from Id in db.Admins where Id.Ad_Email.Equals(SAML.Email) select Id).FirstOrDefault();
            
            if (ad != null)
            {
                if (ad.Password.Equals(SAML.Password))
                {
                    FormsAuthentication.SetAuthCookie(SAML.Email, true);
                    Session.RemoveAll();
                    Session["Email"] = SAML.Email;
                    HttpCookie c1 = new HttpCookie("ADInfo");
                    c1.Value = ad.Ad_Email.ToString();
                    c1.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(c1);
                    if (ReturnUrl != null)
                    {
                        string[] arr = ReturnUrl.Split('/');
                        ViewBag.Message = arr[0].ToString();
                        
                        return RedirectToAction(arr[2].ToString(), arr[1].ToString());
                    }
                    return RedirectToAction("Index", "AD_Home");
                }
                else
                {
                    ViewBag.Message = "Incorrect Password.  ";
                }
                
            }
            else
            {
               ViewBag.Message = "Incorrect Email.";
            }

            return View(SAML);
        }

    }
}
