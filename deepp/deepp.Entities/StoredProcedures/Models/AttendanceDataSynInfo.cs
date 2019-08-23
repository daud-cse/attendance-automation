using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.StoredProcedures.Models
{
   public class AttendanceDataSynInfo
    {
        public int UserInfoId { get; set; }
        public string UserInfoName { get; set; }
        public int InstituteId { get; set; }
        public int AcademicBranchId { get; set; }
        public string AcademicBranchName { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public DateTime? LastAttendanceDate { get; set; }
        public DateTime? LastAttendanceInTimeDate { get; set; }
        public DateTime? LastAttendanceOutTimeDate { get; set; }
        public int AttendanceTypeId { get; set; }
        public string AttendanceTypeName { get; set; }
        public DateTime? LastAttendanceSynDate { get; set; }
       
      
      
     
       

    }
}
