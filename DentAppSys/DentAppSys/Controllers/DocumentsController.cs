using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

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
            return RedirectToAction("Resultlist", doc);  
        }
        public ActionResult Resultlist(Models.GetDoc doc)
        {
            Session["UserEmail"] = "holehole@mail.com"; //for Test
            if (doc.StartDate == DateTime.MinValue && doc.EndDate == DateTime.MinValue && doc.AppID == null)
            {
                using (MaindbModelDataContext db = new MaindbModelDataContext())
                {
                    var patient = db.Patients.FirstOrDefault(u => u.Email == (string)Session["UserEmail"]);
                    var listresult = from r in db.PatientFiles
                                      where r.PatientNo == patient.PatientNo
                                      select r;

                    var TempDoCs = new List<Models.Resultdoc>();
                    foreach (var item in listresult)
                    {
                        var Tempdoc = new Models.Resultdoc();
                        Tempdoc.AppID = item.AppNo;
                        Tempdoc.PatientID = item.PatientNo;
                        Tempdoc.Status = item.Status;
                        TempDoCs.Add(Tempdoc);

                    }
                    return View(TempDoCs);
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
                        var TempDoCs = new List<Models.Resultdoc>();
                        var Tempdoc = new Models.Resultdoc();
                        Tempdoc.AppID = listresult.AppNo;
                        Tempdoc.PatientID = listresult.PatientNo;
                        Tempdoc.Status = listresult.Status;
                        TempDoCs.Add(Tempdoc);
                        //foreach (var item in listresult)
                        //{

                        //    var Tempdoc = new Models.Resultdoc();
                        //    Tempdoc.AppID = item.AppNo;
                        //    Tempdoc.PatientID = item.PatientNo;
                        //    TempDoCs.Add(Tempdoc);

                        //}
                        return View(TempDoCs);
                    }
                }
                else
                {
                    //var Docs = new Models.Resultdoc();
                    //return RedirectToAction("Resultlist", Docs);
                    return View();
                }
            }
        }

    }
}
