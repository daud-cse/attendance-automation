using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deepp.erp.Models
{
    public class LandingViewModel
    {
        public string UserName { get; set; }
        public string InstituteName { get; set; }

        public string CurrentSessionName { get; set; }
        public byte[] InstituteLogo { get; set; }        
    }
}