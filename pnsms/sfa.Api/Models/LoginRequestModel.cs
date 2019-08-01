using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sfa.Api.Models
{
    public class LoginRequestModel
    {

      public  int instituteid { get; set; }
      public string userid { get; set; }
      public string password { get; set; }

    }
}