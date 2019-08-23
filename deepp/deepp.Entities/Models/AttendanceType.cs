using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AttendanceType: Entity
    {
        public AttendanceType()
        {
            this.LeaveApplications = new List<LeaveApplication>();
            this.StudentAttendanceDetails = new List<StudentAttendanceDetail>();
            this.UserAttendanceDetails = new List<UserAttendanceDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsUsedForStudent { get; set; }
        public Nullable<bool> IsUsedForEmployee { get; set; }
        public Nullable<bool> IsUsedForTeacher { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public int ColourId { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsPresent { get; set; }
        public Nullable<bool> IsAbsent { get; set; }
        public Nullable<bool> ShowAtAttendanceEntry { get; set; }
        public Nullable<bool> ShowAtLeaveEntry { get; set; }
        public virtual Colour Colour { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<StudentAttendanceDetail> StudentAttendanceDetails { get; set; }
        public virtual ICollection<UserAttendanceDetail> UserAttendanceDetails { get; set; }
    }
}
