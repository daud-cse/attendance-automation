using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.StoredProcedures.Models
{
   public class AttendanceSummary
    {
       public int AttendanceMonth{get;set;}
       public string AttendanceMonthName { get; set; }
       public int AcademicClassId { get; set; }       
       public string AcademicClassName { get; set; }

       public decimal PercentageOfAttendance { get; set; }
    }
}
