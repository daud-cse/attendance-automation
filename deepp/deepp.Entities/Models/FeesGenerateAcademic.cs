using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FeesGenerateAcademic: Entity
    {
        public int Id { get; set; }
        public int FeesGenerateId { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public Nullable<int> AcademicBranchId { get; set; }
        public Nullable<int> AcademicClassId { get; set; }
        public Nullable<int> AcademicShiftId { get; set; }
        public Nullable<int> AcademicSectionId { get; set; }
        public Nullable<int> AcademicVerssionId { get; set; }
        public Nullable<int> AcademicGroupId { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual AcademicSection AcademicSection { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual AcademicVersion AcademicVersion { get; set; }
        public virtual FeesGenerate FeesGenerate { get; set; }
    }
}
