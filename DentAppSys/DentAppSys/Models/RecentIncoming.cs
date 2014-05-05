using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentAppSys.Models
{
    public class RecentIncoming {

   
            public IEnumerable<DentAppSys.Models.AppModel> RecentAppts {get; set;}
            public IEnumerable<DentAppSys.Models.AppModel> IncomingAppts {get; set;}

            public RecentIncoming() { }
          
     }
    
        }
