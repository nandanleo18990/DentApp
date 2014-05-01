using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentAppSys.Controllers
{
    public class AppointmentController : Controller
    {
        //
        // GET: /Appointment/

        public ActionResult Recent()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Recent(Models.AppModel User)
        {
            if (Session["UserEmail"] != null)
            {
                using (var db = new MaindbModelDataContext())
                {  var patient = db.Patients.FirstOrDefault(u => u.Email ==(String)Session["UserEmail"]);
                    var listrecent = from y in db.Appointments
                                      where y.PatientNo == patient.PatientNo
                                      select y;
                    var TempRecent=new List<Models.AppModel>();
                    foreach(var item in listrecent)
                                      {
                        var Temp = new Models.AppModel();
                        Temp.AppNo = item.AppNo;
                        Temp.Description = item.Description;
                                      }
                    return View(listrecent);
             }
            }
            return View();
        }



        public ActionResult Make()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Make(Models.AppModel User)
        {
            if (Session["UserEmail"] != null)
            {
                using (var db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    var app = new Appointment();
                   // if (app.Date > DateTime.Now.Date)
                    //{
                        app.Date = User.Date;
                        app.Description = User.Description;
                        app.Status = "isPending";
                        app.PatientNo = patient.PatientNo;
                        app.AppNo = Guid.NewGuid().GetHashCode();
                        db.Appointments.InsertOnSubmit(app);
                        db.SubmitChanges();
                    /* }
                    else
                    {
                        ModelState.AddModelError("", "Please choose valid date!!!");
                    }*/
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
      return RedirectToAction("Make", "Appointment");  }

    }
}

    
