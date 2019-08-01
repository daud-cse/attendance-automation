using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
  public  class MaleFemaleRatioInstituteWise
    {

      public int GenderId { get; set; }
      public string GenderName { get; set; }
      public int CountGenderWise { get; set; }
    }
}
