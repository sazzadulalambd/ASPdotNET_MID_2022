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
            if (Session["In_Email"] != null)
            {
                var email = Session["In_Email"].ToString();
                var db = new bePartnerCentralDatabaseEntities2();
                var In = (from I in db.Investors where I.In_Email.Equals(email) select I).FirstOrDefault();
                ViewBag.Message = email;
                return View(In);
            }
            return RedirectToAction("InLogin", "Investor");
        }


        public ActionResult InStartups()
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var data = (from I in db.Ideas where I.Status.Equals("Posted") select I).ToList();
            return View(data);
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

        [HttpGet]
        public ActionResult InMessenger()
        {
            var email = Session["In_Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();
            var msgs = (from I in db.Messages where I.Sender.Equals(email) || I.Receiver.Equals(email) select I).ToList();

            List<InvestorMessage> listmsg = new List<InvestorMessage>();
            

            foreach(var m in msgs)
            {
                var msg = new InvestorMessage();
                msg.MsgId = m.MsgId;
                msg.Sender = m.Sender;
                msg.Receiver = m.Receiver;
                msg.Message = m.Message1;
                msg.Status = m.Status;
                msg.Ttime = m.Time;

                if (m.Sender.Equals(email))
                {
                    var en = (from I in db.Entrepreneurs where I.En_Email.Equals(m.Receiver) select I).FirstOrDefault();
                    msg.ReceiverName = en.FirstName + " " + en.LastName;
                    msg.ReceiverOccupation = en.Occupation;
                    msg.ReceiverImg = en.Img;
                    msg.ReceiverPhone = en.Phone;

                    var com = (from I in db.Ideas where I.En_Post_Email.Equals(m.Receiver) select I).FirstOrDefault();
                    msg.ReceiverCompany = com.Company_Name;

                    msg.SenderName = null;
                    msg.SenderOccupation = null;
                    msg.SenderImg = null;
                    msg.SenderCompany = null;
                }
                else
                {
                    var en = (from I in db.Entrepreneurs where I.En_Email.Equals(m.Sender) select I).FirstOrDefault();
                    msg.SenderName = en.FirstName + " " + en.LastName;
                    msg.SenderOccupation = en.Occupation;
                    msg.SenderImg = en.Img;
                    msg.SenderPhone = en.Phone;

                    var com = (from I in db.Ideas where I.En_Post_Email.Equals(m.Sender) select I).FirstOrDefault();
                    msg.SenderCompany = com.Company_Name;

                    msg.ReceiverName = null;
                    msg.ReceiverOccupation = null;
                    msg.ReceiverImg = null;
                    msg.ReceiverCompany = null;
                }
                listmsg.Add(msg);
            }

            return View(listmsg);
        }


        [HttpPost]
        public ActionResult InMessenger(InvestorSend InSend)
        {

            var email = Session["In_Email"].ToString();
            var db = new bePartnerCentralDatabaseEntities2();

            if (ModelState.IsValid)
            {
                string[] arr = InSend.Receiver.Split(' ');
                InSend.time = DateTime.Now.ToString();
                InSend.Status = "Sent";
                InSend.Sender = email;
                InSend.Receiver = arr[1];

                if (InSendMessage(InSend))
                {
                    ViewBag.Message = "Data Inserted";
                }
                else
                {
                    ViewBag.Message = "Something Went Wrong";
                }

            }
            else
            {
                ViewBag.Message = "Invalid";
            }
            
            
            var msgs = (from I in db.Messages where I.Sender.Equals(email) || I.Receiver.Equals(email) select I).ToList();

            List<InvestorMessage> listmsg = new List<InvestorMessage>();


            foreach (var m in msgs)
            {
                var msg = new InvestorMessage();
                msg.MsgId = m.MsgId;
                msg.Sender = m.Sender;
                msg.Receiver = m.Receiver;
                msg.Message = m.Message1;
                msg.Status = m.Status;
                msg.Ttime = m.Time;

                if (m.Sender.Equals(email))
                {
                    var en = (from I in db.Entrepreneurs where I.En_Email.Equals(m.Receiver) select I).FirstOrDefault();
                    msg.ReceiverName = en.FirstName + " " + en.LastName;
                    msg.ReceiverOccupation = en.Occupation;
                    msg.ReceiverImg = en.Img;
                    msg.ReceiverPhone = en.Phone;

                    var com = (from I in db.Ideas where I.En_Post_Email.Equals(m.Receiver) select I).FirstOrDefault();
                    msg.ReceiverCompany = com.Company_Name;

                    msg.SenderName = null;
                    msg.SenderOccupation = null;
                    msg.SenderImg = null;
                    msg.SenderCompany = null;
                }
                else
                {
                    var en = (from I in db.Entrepreneurs where I.En_Email.Equals(m.Sender) select I).FirstOrDefault();
                    msg.SenderName = en.FirstName + " " + en.LastName;
                    msg.SenderOccupation = en.Occupation;
                    msg.SenderImg = en.Img;
                    msg.SenderPhone = en.Phone;

                    var com = (from I in db.Ideas where I.En_Post_Email.Equals(m.Sender) select I).FirstOrDefault();
                    msg.SenderCompany = com.Company_Name;

                    msg.ReceiverName = null;
                    msg.ReceiverOccupation = null;
                    msg.ReceiverImg = null;
                    msg.ReceiverCompany = null;
                }
                listmsg.Add(msg);
            }

            return View(listmsg);
        }

        public ActionResult InBusinessDetails(string Id)
        {
            var db = new bePartnerCentralDatabaseEntities2();
            var Ed = (from I in db.Ideas where I.Company_Name.Equals(Id) select I).FirstOrDefault();
            if(Ed != null)
            {
                return View(Ed);
            }
            return RedirectToAction("InStartups");
        }

        //**************************************** DATABASE STARTS ****************************************

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
                Session["In_Email"] = In.In_Email;
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

        public bool InSendMessage(InvestorSend m)
        {
            var msg = new Message()
            {
                Sender = m.Sender,
                Receiver = m.Receiver,
                Message1 = m.Message,
                Status = m.Status,
                Time = m.time,
                
            };
            var db = new bePartnerCentralDatabaseEntities2();
            db.Messages.Add(msg);
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