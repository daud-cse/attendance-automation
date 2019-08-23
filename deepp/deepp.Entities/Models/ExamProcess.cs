using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ExamProcess: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicSessionId { get; set; }
        public int ExamTypeId { get; set; }
        public int AcademicClassesId { get; set; }
        public int AcademicBranchId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public Nullable<int> AcademicSectionId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> RunExamProcessAt { get; set; }
        public Nullable<System.DateTime> RunReportCardProcessAt { get; set; }
        public Nullable<System.DateTime> RunConsolidateReportProcessAt { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicSection AcademicSection { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual ExamType ExamType { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
