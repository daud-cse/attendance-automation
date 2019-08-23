using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class StudentAttendanceDetail: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int StudentAttendanceId { get; set; }
        public int StudentId { get; set; }
        public int AttendanceTypeId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> IsAbsconding { get; set; }
        public string Comments { get; set; }
        public Nullable<int> LocalId { get; set; }
        public virtual AttendanceType AttendanceType { get; set; }
        public virtual StudentAttendance StudentAttendance { get; set; }
        public virtual Student Student { get; set; }
    }
}
