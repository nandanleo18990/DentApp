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
        [HttpPost]
        public ActionResult Index(Models.GetDoc doc)
        {
            if (doc == null)
            {
                using (MaindbModelDataContext db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (string)Session["UserEmail"]);
                    var listresult = (from r in db.PatientFiles
                                      where r.PatientNo == patient.PatientNo
                                      select r).ToList();
                    Models.ResultDocs Docs = new Models.ResultDocs(); ;
                    Docs.Resultdocs = listresult;
                    Docs.Resultdoc = listresult.FirstOrDefault();
                    return RedirectToAction("Resultlist", null, Docs);
                }
            }
            else
            {
                if (doc.AppID != null)
                {
                    try
                    {
                       int AppNo = Convert.ToInt32(doc.AppID);
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Please Input Correct Appointment Number !!!");
                        return View();
                    }
                    using (MaindbModelDataContext db = new MaindbModelDataContext())
                    {
                        var listresult = db.PatientFiles.FirstOrDefault(u => u.AppNo == Convert.ToInt32(doc.AppID));
                        Models.ResultDocs Docs = new Models.ResultDocs();
                        Docs.Resultdoc = listresult;
                        return RedirectToAction("Resultlist", null, Docs);
                    }
                }
                else
                {
                    Models.ResultDocs Docs = new Models.ResultDocs();
                    return RedirectToAction("Resultlist", null, Docs);
                }
            }
            
        }
        public ActionResult Resultlist(Models.ResultDocs ReSDoCs)
        {
            return View();
        }

    }
}
