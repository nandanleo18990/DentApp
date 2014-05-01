using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentAppSys.Models
{
    public class AppModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date: ")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Appointment No: ")]
        public int AppNo { get; set; }
        [Required]
        [Display(Name = "Patient No: ")]
        public int PatientNo { get; set; }

        [Required]
        [Display(Name = "Status: ")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Doctor Description: ")]
        public string DrDescription { get; set; }

        [Required]
        [Display(Name = "Description: ")]
        public string Description { get; set; }
    }

   

  
}
