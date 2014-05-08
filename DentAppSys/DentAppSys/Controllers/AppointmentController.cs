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
            if (Session["UserEmail"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }

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
                    app.Date = (DateTime)User.Date;
                    app.Description = User.Description;
                    app.Status = "isPending";
                    app.PatientNo = patient.PatientNo;
                    app.AppNo = Guid.NewGuid().GetHashCode();
                    db.Appointments.InsertOnSubmit(app);
                    db.SubmitChanges();
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Make", "Appointment");
        }
        //("Index", "Patient")
    }
}


