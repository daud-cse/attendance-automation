using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.attendance.Model
{
  public  class UserAttendanceDetail
    {
        public int Id { get; set; }
        public int UserAttendanceId { get; set; }
        public int UserInfoId { get; set; }
        public int AttendanceTypeId { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}
