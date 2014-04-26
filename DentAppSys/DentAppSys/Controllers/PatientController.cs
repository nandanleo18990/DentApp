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
        public ActionResult Index(Models.RegisterModel User)
        {
            Session["UserEmail"] = "AllowToOpenPage";
            if (Session["UserEmail"] != null)
            {
                string Email = (string)Session["UserEmail"];

                return View(User);
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }
        }

    }
}
