using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentAppSys.Models
{
    public class GetDoc
    {
        [Display(Name = "Appointment ID = ")]
        public string AppID { get; set; }
        public int PatientID { get; set; }
        [Display(Name = "Date From = ")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "To  = ")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class Resultdoc
    {
        [Display(Name = "Appointment ID = ")]
        public int AppID { get; set; }
        [Display(Name = " Patient ID = ")]
        public int PatientID { get; set; }
        [Display(Name = "Date = ")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Prescription = ")]
        public string Prescription { get; set; }
        [Display(Name = "Treatments Details = ")]
        public string TreatmentDis { get; set; }
        [Display(Name = "Status = ")]
        public string Status { get; set; }
        [Display(Name = "Related Image = ")]
        public byte[] Image { get; set; }
    }

}