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
            if (Session["UserEmail"] != null)
            {
                string Email = (string)Session["UserEmail"];

                using (var db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    if (patient != null)
                    {
                        ViewBag.FirstName = patient.Name;
                        ViewBag.LastName = patient.Surname;
                        ViewBag.BirthDate = patient.Birthday;
                        ViewBag.Email = patient.Email;
                    }
                    else
                    {
                        return RedirectToAction("RegAndLogin", "User");
                    }

                }



                using (var db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    var listincoming = (from y in db.Appointments
                                        where y.PatientNo == patient.PatientNo
                                        where y.Date > DateTime.Today
                                        orderby y.Date descending
                                        select y).Take(5);

                    var TempIncoming = new List<Models.AppModel>();
                    foreach (var item in listincoming)
                    {
                        var Temp1 = new Models.AppModel();
                        Temp1.AppNo = item.AppNo;
                        Temp1.PatientNo = (Int32)item.PatientNo;
                        Temp1.Date = (DateTime)item.Date;
                        Temp1.Status = item.Status;
                        Temp1.Description = item.Description;
                        TempIncoming.Add(Temp1);


                    }

                    var p = db.Patients.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    var listrecent = (from y in db.Appointments
                                      where y.PatientNo == p.PatientNo
                                      where y.Status == "isConfirmed"
                                      orderby y.Date descending
                                      select y).Take(5);

                    var TempRecent = new List<Models.AppModel>();
                    foreach (var item in listrecent)
                    {
                        var Temp2 = new Models.AppModel();
                        Temp2.AppNo = item.AppNo;
                        Temp2.PatientNo = (Int32)item.PatientNo;
                        Temp2.Date = (DateTime)item.Date;
                        Temp2.Status = item.Status;
                        Temp2.Description = item.Description;
                        TempRecent.Add(Temp2);

                    }
                    return View(new DentAppSys.Models.RecentIncoming() { RecentAppts = TempRecent, IncomingAppts = TempIncoming });
                }
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }
        }

    }
}
