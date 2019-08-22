using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class UserAttendanceDetail: Entity
    {
        public int Id { get; set; }
        public int UserAttendanceId { get; set; }
        public int UserInfoId { get; set; }
        public int AttendanceTypeId { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual AttendanceType AttendanceType { get; set; }
        public virtual UserAttendance UserAttendance { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
