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



        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Recent(Models.AppModel User)
        {
            if (Session["UserEmail"] != null)
            {
                using (var db = new MaindbModelDataContext())
                {

                    var patient = db.Patients.FirstOrDefault(u => u.Email == (string)Session["UserEmail"]);
                    var listrecent = from r in db.Patients
                                     where r.PatientNo == patient.PatientNo
                                     select r;

                    var TempRecent = new List<Models.AppModel>();
                    foreach (var item in listrecent)
                    {
                        var Temp = new Models.AppModel();
                        Temp.AppID = item.AppNo;

                        Temp.AppID = item.AppNo;
                        Temp.PatientID = item.PatientNo;
                        TempRecent.Add(Temp);

                    }
                    return View(TempRecent);
                }
            }
        }

                    
           //  Where (m => m.Date < Current.Date).OrderByDescending(m => m.Date).Take(20).ToList();
                 
                     //var list = db.Appointments.ToList();

                      /*var patient = db.Patients.FirstOrDefault(u => u.Email ==(String)Session["UserEmail"]);
                      var list = (from m in db.Appointments
                                  where m.PatientNo == patient.PatientNo
                                  select m).ToList();
                                 }}
                      return View(list.ToList());
                 }
           
       */
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
                    if (app.Date > DateTime.Now.Date)
                    {
                        app.Date = User.Date;
                        app.Description = User.Description;
                        app.Status = "isPending";
                        app.PatientNo = patient.PatientNo;
                        app.AppNo = Guid.NewGuid().GetHashCode();
                        db.Appointments.InsertOnSubmit(app);
                        db.SubmitChanges();
                     }
                    else
                    {
                        ModelState.AddModelError("", "Please choose valid date!!!");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
      return RedirectToAction("Make", "Appointment");  }

    }
}

    
