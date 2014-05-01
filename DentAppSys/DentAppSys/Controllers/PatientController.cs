using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentAppSys.Controllers
{
    public class PatientController : Controller
    {
        //
        // GET: /Patient/
        public ActionResult Index()
        {
            if (Session["UserEmail"] != null) //for Test
            {
                string Email = (string)Session["UserEmail"];

                using (var db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    ViewBag.FirstName = patient.Name;
                    ViewBag.LastName = patient.Surname;
                    ViewBag.BirthDate = patient.Birthday;
                    ViewBag.Email = patient.Email;
                
                
                }
             

                using (var db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    var listrecent = from y in db.Appointments
                                     where y.PatientNo == patient.PatientNo
                                     select y;
                    var TempRecent = new List<Models.AppModel>();
                    foreach (var item in listrecent)
                    {
                        var Temp = new Models.AppModel();
                        Temp.AppNo = item.AppNo;
                        Temp.PatientNo = (Int32)item.PatientNo;
                        Temp.Date = (DateTime)item.Date;
                        Temp.Status = item.Status;
                        Temp.Description = item.Description;
                        TempRecent.Add(Temp);

                    }
                    return View(TempRecent);
                }
                
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }
        }

    }
}
