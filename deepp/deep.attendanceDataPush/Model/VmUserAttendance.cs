using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.attendance.Model
{
   public class VmUserAttendance
    {

        public UserAttendance UserAttendance { get; set; }
        public List<UserAttendanceDetail> AttendanceDetails { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
