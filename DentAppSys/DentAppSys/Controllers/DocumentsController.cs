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
        public ActionResult Index(Models.GetDoc doc)
        {
            return RedirectToAction("Resultlist", doc);
        }
        public ActionResult Resultlist(Models.GetDoc doc)
        {
            
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
                        Tempdoc.Prescription = item.Prescription;
                        Tempdoc.TreatmentDis = item.TreatmentDetails;
                        //var base64 = Convert.ToBase64String(item.Image.ToArray());
                        Tempdoc.Image = item.Image.ToArray();
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
                        Tempdoc.Prescription = listresult.Prescription;
                        Tempdoc.TreatmentDis = listresult.TreatmentDetails;
                        Tempdoc.Image = listresult.Image.ToArray();
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
                    using (MaindbModelDataContext db = new MaindbModelDataContext())
                    {
                        var patient = db.Patients.FirstOrDefault(u => u.Email == (string)Session["UserEmail"]);
                        var listresult = from App in db.Appointments
                                         where App.Date >= doc.StartDate && App.Date <= doc.EndDate && App.PatientNo == patient.PatientNo
                                         join Doc in db.PatientFiles on App.AppNo equals Doc.AppNo into ResGroup
                                         from res in ResGroup
                                         select new { AppNo = res.AppNo, PatientNo = res.PatientNo, Status = res.Status, Prescription = res.Prescription , TreatmentDetails = res.TreatmentDetails , Image = res.Image};
                                         
                        var TempDoCs = new List<Models.Resultdoc>();
                        foreach (var item in listresult)
                        {
                            var Tempdoc = new Models.Resultdoc();
                            Tempdoc.AppID = item.AppNo;
                            Tempdoc.PatientID = item.PatientNo;
                            Tempdoc.Status = item.Status;
                            Tempdoc.Prescription = item.Prescription;
                            Tempdoc.TreatmentDis = item.TreatmentDetails;
                            //var base64 = Convert.ToBase64String(item.Image.ToArray());
                            Tempdoc.Image = item.Image.ToArray();
                            TempDoCs.Add(Tempdoc);

                        }

                        
                        return View(TempDoCs);
                    }
                }
            }
        }

    }
}
