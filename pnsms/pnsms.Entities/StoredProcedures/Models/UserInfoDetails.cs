using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
   public  class UserInfoDetails
    {


       public int UserInfoId { get; set; }
       public int InstituteId { get; set; }
       public string InstituteName { get; set; }

       public int AcademicSessionId { get; set; }
       public string UserName { get; set; }
       public bool IsActive { get; set; }

    }
}
