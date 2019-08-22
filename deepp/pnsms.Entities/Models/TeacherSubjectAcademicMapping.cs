using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class TeacherSubjectAcademicMapping: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public Nullable<int> AcademicBranchId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public Nullable<int> AcademicClassId { get; set; }
        public Nullable<int> AcademicSectionId { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public Nullable<int> SubjectSplitId { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public Nullable<int> SubjectGroupId { get; set; }
        public string Description { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicSection AcademicSection { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
