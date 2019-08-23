using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class StudentAttendance: Entity
    {
        public StudentAttendance()
        {
            this.StudentAttendanceDetails = new List<StudentAttendanceDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public int TeacherId { get; set; }
        public int AcademicSessionId { get; set; }
        public int AcademicBranchId { get; set; }
        public int AcademicClassId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public Nullable<int> AcademicSectionId { get; set; }
        public Nullable<int> AcamedicGroupId { get; set; }
        public Nullable<int> SubjectAcademicClassMappingsId { get; set; }
        public Nullable<int> AcademicPeriodId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public Nullable<int> PresentCount { get; set; }
        public Nullable<int> AbsentCount { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public Nullable<int> AbscondingCount { get; set; }
        public Nullable<decimal> PresentPercentage { get; set; }
        public Nullable<decimal> AbsentPercentage { get; set; }
        public Nullable<decimal> AbscondingPercentage { get; set; }
        public Nullable<int> LocalId { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicClassSectionMapping AcademicClassSectionMapping { get; set; }
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual AcademicPeriod AcademicPeriod { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual ICollection<StudentAttendanceDetail> StudentAttendanceDetails { get; set; }
        public virtual SubjectAcademicClassMapping SubjectAcademicClassMapping { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
