using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class SubjectAcademicClassMappingSubjectType: Entity
    {
        public int Id { get; set; }
        public int SubjectAcademicClassMappingId { get; set; }
        public int InstituteId { get; set; }
        public int AcademicClassId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectTypeId { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SubjectAcademicClassMapping SubjectAcademicClassMapping { get; set; }
        public virtual SubjectType SubjectType { get; set; }
    }
}
