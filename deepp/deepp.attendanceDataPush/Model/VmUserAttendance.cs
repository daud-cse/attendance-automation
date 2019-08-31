using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfa.attendance.Model
{
   public class VmUserAttendance
    {

        public UserAttendance UserAttendance { get; set; }
        public List<UserAttendanceDetail> AttendanceDetails { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
