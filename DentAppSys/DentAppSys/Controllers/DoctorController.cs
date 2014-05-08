using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentAppSys.Controllers
{
    public class DoctorController : Controller
    {
        //
        // GET: /Doctor/

        public ActionResult Index()
        {
            if (Session["UserEmail"].ToString().Contains("@dentappsys.com"))
            {
                string Email = (string)Session["UserEmail"];

                using (var db = new MaindbModelDataContext())
                {
                    var Doctor = db.Doctors.FirstOrDefault(u => u.Email == (String)Session["UserEmail"]);
                    if (Doctor != null)
                    {
                        ViewBag.FirstName = Doctor.Name;
                        ViewBag.LastName = Doctor.Surname;
                        ViewBag.BirthDate = Doctor.BirthDate;
                        ViewBag.Email = Doctor.Email;

                        return View();
                    }
                    else
                    {
                        return RedirectToAction("RegAndLogin", "User");
                    }
                }
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }

        }

        public ActionResult Management()
        {
            if (Session["UserEmail"].ToString().Contains("@dentappsys.com"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }


        }
        [HttpPost]
        public ActionResult Management(Models.Drmanagments Tempmanage)
        {
            if (Session["UserEmail"].ToString().Contains("@dentappsys.com"))
            {
                if (Tempmanage.appmodel != null)
                {
                    using (var db = new MaindbModelDataContext())
                    {
                        var app = db.Appointments.FirstOrDefault(u => u.AppNo == Tempmanage.appmodel.AppNo);
                        if (app != null)
                        {
                            app.Status = Tempmanage.appmodel.Status;
                            app.DrDescription = Tempmanage.appmodel.DrDescription;
                            db.SubmitChanges();

                            ViewBag.ChangeResultApp = "Done !";
                            return View();
                        }
                        else
                        {
                            ViewBag.ChangeResultApp = "Check your appointment number then try again !";
                            return View();
                        }
                    }
                }
                else
                {
                    if (Tempmanage.resultdoc != null)
                    {
                        using (var db = new MaindbModelDataContext())
                        {
                            var app = db.PatientFiles.FirstOrDefault(u => u.AppNo == Tempmanage.resultdoc.AppID && u.PatientNo == Tempmanage.resultdoc.PatientID);
                            if (app == null)
                            {
                                var tempapp = new PatientFile();
                                tempapp.AppNo = Tempmanage.resultdoc.AppID;
                                tempapp.PatientNo = Tempmanage.resultdoc.PatientID;
                                tempapp.Prescription = Tempmanage.resultdoc.Prescription;
                                tempapp.Status = Tempmanage.resultdoc.Status;
                                tempapp.TreatmentDetails = Tempmanage.resultdoc.TreatmentDis;
                                db.PatientFiles.InsertOnSubmit(tempapp);
                                db.SubmitChanges();

                                ViewBag.ChangeResultDoc = "Done !";
                                return View();
                            }
                            else
                            {
                                ViewBag.ChangeResultDoc = "Check your appointment number or patient number then try again !";
                                return View();
                            }

                        }

                    }
                    else
                    {
                        return View();
                    }

                }
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }


        }

        //[HttpPost]
        //public ActionResult Management(Models.Resultdoc Tempapp)
        //{
        //    if (Session["UserEmail"].ToString().Contains("@dentappsys.com"))
        //    {
        //        using (var db = new MaindbModelDataContext())
        //        {
        //            var app = db.PatientFiles.FirstOrDefault(u => u.AppNo == Tempapp.AppID && u.PatientNo == Tempapp.PatientID);
        //            if (app == null)
        //            {
        //                app.AppNo = Tempapp.AppID;
        //                app.PatientNo = Tempapp.PatientID;
        //                app.Prescription = Tempapp.Prescription;
        //                app.Status = Tempapp.Status;
        //                app.TreatmentDetails = Tempapp.TreatmentDis;
        //                db.PatientFiles.InsertOnSubmit(app);
        //                db.SubmitChanges();

        //                ViewBag.ChangeResult = "Done !";
        //                return View();
        //            }
        //            else
        //            {
        //                ViewBag.ChangeResult = "Check your appointment number or patient number then try again !";
        //                return View();
        //            }
        //        }

        //    }
        //    else
        //    {
        //        return RedirectToAction("RegAndLogin", "User");
        //    }
        //}


    }
}
