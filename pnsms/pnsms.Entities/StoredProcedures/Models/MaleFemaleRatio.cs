using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
   public class MaleFemaleRatio
    {
       public int InstituteId { get; set; }
       public string InstituteName { get; set; }
       public int Total { get; set; }
       public int Male { get; set; }    
       public int Female { get; set; }
       public decimal FemalePercentage { get; set; }
       public decimal MalePercentage { get; set; }
    }
}
