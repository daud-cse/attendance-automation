﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.StoredProcedures.Models
{
   public  class InstituteTotalInfo
    {
       public int TotalStudentsCount {get;set;}
       public int TotalTeachersCount {get;set;}
       public int TotalEmployeesCount {get;set;}
       public int TotalTeachersRequired {get;set;}
       public int TotalAvailableSMS {get;set;}	     
       public int TotalInstitute { get; set; }
    }
}