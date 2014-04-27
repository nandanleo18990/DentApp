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
        [Display(Name = "Description: ")]
        public string Description { get; set; }
    }
}
