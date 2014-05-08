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

    }
}
