using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicCalendar: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public Nullable<int> AcademicBranchId { get; set; }
        public int AcademicSessionId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsInstituteClose { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> IsClassSuspend { get; set; }
        public Nullable<bool> IsClassSuspendAllBranch { get; set; }
        public Nullable<bool> IsClassSuspendAllClass { get; set; }
        public Nullable<bool> IsClassSuspendAllShift { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
