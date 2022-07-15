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
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignIn(EmployeeSignIn emsingin, string ReturnUrl)
        {

            var db = new bePartnerCentralDatabaseEntities2();
            var em = (from Id in db.Employees where Id.Emp_Email.Equals(emsingin.Email) select Id).FirstOrDefault();

            if (em != null)
            {
                if(em.Password.Equals(emsingin.Password))
                {
                    if (em.Status.Equals("Valid"))
                    {
                        FormsAuthentication.SetAuthCookie(emsingin.Email, true);
                        Session.RemoveAll();
                        Session["Email"] = emsingin.Email;
                        HttpCookie c1 = new HttpCookie("EMInfo");
                        c1.Value = em.Emp_Email.ToString();
                        c1.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(c1);
                        if (ReturnUrl != null)
                        {
                            string[] arr = ReturnUrl.Split('/');
                            ViewBag.Message = arr[0].ToString();

                            return RedirectToAction(arr[2].ToString(), arr[1].ToString());
                        }
                        return RedirectToAction("Index", "J_Employee");
                    }
                    else
                    {
                        ViewBag.Message = "Your account is not valid please wait! ";
                    }
                }
                
                else
                {
                    ViewBag.Message = "Incorrect Password";
                }

            }
            else
            {
                ViewBag.Message = "Incorrect Email.";
            }

            return View(emsingin);
        }






        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(EmployeeRegistration emreg)
        {
            if (ModelState.IsValid)
            {
                var db = new bePartnerCentralDatabaseEntities2();
                var add_emp = new Employee()
                {
                    FirstName = emreg.FirstName,
                    LastName = emreg.LastName,
                    Emp_Email=emreg.Email,
                    Dob=emreg.Dob,
                    Address=emreg.Address,
                    Phone=emreg.Phone,
                    Password=emreg.ConfrimPassword,
                    Security_key=emreg.Security_key,
                    Gender=emreg.Gender,
                    Status= "Invalid",
                    Img="",
                    Nid=emreg.Nid
                    
                };

                db.Employees.Add(add_emp);

                db.SaveChanges();

                return RedirectToAction("SignIn");

            }
            return View();
        }

    }
}
    