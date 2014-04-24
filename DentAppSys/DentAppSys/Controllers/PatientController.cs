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
        public ActionResult Index(string FirstName, string LastName)
        {
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            return View();
        }

    }
}
