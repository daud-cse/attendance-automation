﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deepp.Api.Models
{
    public class LoginReponseModel
    {

       public string Success { get; set; }
       public string Message { get; set; }
       public string Token { get; set; }
       public object Obj { get; set; }
    }
   
}