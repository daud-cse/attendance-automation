using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.Attendance
{
  public  class AttendanceProcessModel
    {
        public string deviceInfo { get; set; }
        public string attendanceDate { get; set; }
        public int attendanceUserTypeId { get; set; }
    }
}
