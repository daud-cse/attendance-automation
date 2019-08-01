using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
   public  class RightsDetails
    {
        public int UserInfoId { get; set; }
       public int RoleId { get; set; }
       public string RightId { get; set; }

       public string Name { get; set; }
       public string code { get; set; }
       public bool IsActive { get; set; }      
    }
}
