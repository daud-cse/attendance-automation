﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfa.attendance.Model
{
    public class LoginRequestModel
    {
        public int instituteid { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
    }
}
