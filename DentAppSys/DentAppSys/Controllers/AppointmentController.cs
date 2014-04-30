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
                  

                   var patient = db.Patients.FirstOrDefault(u => u.Email ==(String)Session["UserEmail"]);
                    var app = new Appointment();
                    app.Date = User.Date;
                    app.Description = User.Description;
                    app.Status = "true";
                    app.PatientNo = patient.PatientNo;
                    db.Appointments.InsertOnSubmit(app);
                    db.SubmitChanges();
                    return RedirectToAction("Make", "Appointment");
                }

            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

    }
}

