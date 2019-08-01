using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class UserAttendance: Entity
    {
        public UserAttendance()
        {
            this.UserAttendanceDetails = new List<UserAttendanceDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public int UserInfoTypeId { get; set; }
        public int AcademicBranchId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public Nullable<int> PresentCount { get; set; }
        public Nullable<int> AbsentCount { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public Nullable<decimal> PresentPercentage { get; set; }
        public Nullable<decimal> AbsentPercentage { get; set; }
        public Nullable<System.DateTime> LastAttendanceSynDate { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual ICollection<UserAttendanceDetail> UserAttendanceDetails { get; set; }
        public virtual UserInfoType UserInfoType { get; set; }
    }
}
