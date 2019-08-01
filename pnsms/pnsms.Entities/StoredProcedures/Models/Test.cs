using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
   public class Test
    {
       public List<AddressType> AddressTypes { get; set; }
       public List<Department> Departments { get; set; }
     
    }
}
