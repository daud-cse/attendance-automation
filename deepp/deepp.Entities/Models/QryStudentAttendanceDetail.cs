using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class QryStudentAttendanceDetail: Entity
    {
        public int StudentAttendanceId { get; set; }
        public int StudentId { get; set; }
        public int AttendanceTypeId { get; set; }
        public Nullable<bool> IsAbsconding { get; set; }
        public string Flag { get; set; }
        public Nullable<bool> IsPresent { get; set; }
        public Nullable<System.DateTime> AttendanceDate { get; set; }
    }
}
