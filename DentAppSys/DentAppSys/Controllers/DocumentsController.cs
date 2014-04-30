using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentAppSys.Controllers
{
    public class DocumentsController : Controller
    {
        //
        // GET: /Documents/

        public ActionResult Index()
        {
            if (Session["UserEmail"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("RegAndLogin", "User");
            }
        }
        [HttpGet]
        public ActionResult Index(Models.GetDoc doc)
        {
            return View();
        }
        public ActionResult Resultlist()
        {
            return View();
        }
        public ActionResult Resultlist(Models.ResultDoc doc)
        {
            return View();
        }
        
    }
}
