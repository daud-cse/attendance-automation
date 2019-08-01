using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
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
