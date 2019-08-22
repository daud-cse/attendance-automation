using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AcademicSection: Entity
    {
        public AcademicSection()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.ExamProcesses = new List<ExamProcess>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.Routines = new List<Routine>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
    }
}
