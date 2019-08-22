using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
   public  class VmUserInfoDetails
    {
      
       public UserInfoDetails objUserInfoDetails { get; set; }
       public List<RightsDetails> RightsDetails { get; set; }
    }
}
