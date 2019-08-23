using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicGroup: Entity
    {
        public AcademicGroup()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.Routines = new List<Routine>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
    }
}
