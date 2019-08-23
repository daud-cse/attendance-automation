using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class SubjectType: Entity
    {
        public SubjectType()
        {
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
            this.SubjectAcademicClassMappingSubjectTypes = new List<SubjectAcademicClassMappingSubjectType>();
            this.SubjectStudentMappings = new List<SubjectStudentMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> IsFailApplicable { get; set; }
        public decimal HandicapGradePoint { get; set; }
        public decimal HandicapMarks { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public virtual ICollection<SubjectAcademicClassMappingSubjectType> SubjectAcademicClassMappingSubjectTypes { get; set; }
        public virtual ICollection<SubjectStudentMapping> SubjectStudentMappings { get; set; }
    }
}
