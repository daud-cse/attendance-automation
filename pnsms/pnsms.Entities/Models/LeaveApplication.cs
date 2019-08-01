using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class LeaveApplication: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicBranchId { get; set; }
        public int UserInfoId { get; set; }
        public int AttendanceTypeId { get; set; }
        public System.DateTime ApplyDate { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public bool IsHalfDay { get; set; }
        public Nullable<bool> IsFirstHalf { get; set; }
        public Nullable<bool> IsSecondHalf { get; set; }
        public decimal LeaveCount { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AttendanceType AttendanceType { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
