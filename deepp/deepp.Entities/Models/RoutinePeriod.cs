using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class RoutinePeriod: Entity
    {
        public RoutinePeriod()
        {
            this.Diaries = new List<Diary>();
            this.RoutineDetails = new List<RoutineDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public bool IsLeasure { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> AcademicBranchId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public int OrderBy { get; set; }
        public Nullable<int> RoutinePeriodTypeId { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails { get; set; }
        public virtual RoutinePeriodType RoutinePeriodType { get; set; }
    }
}
