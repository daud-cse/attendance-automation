using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.attendance.Model
{
   public class UserAttendance
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int UserInfoTypeId { get; set; }
        public int AcademicBranchId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public Nullable<int> PresentCount { get; set; }
        public Nullable<int> AbsentCount { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public Nullable<decimal> PresentPercentage { get; set; }
        public Nullable<decimal> AbsentPercentage { get; set; }
        public DateTime? LastAttendanceSynDate { get; set; }
    }
}
