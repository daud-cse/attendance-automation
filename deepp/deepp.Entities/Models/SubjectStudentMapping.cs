using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class SubjectStudentMapping: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicSessionId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public Nullable<int> SubjectTypeId { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SubjectType SubjectType { get; set; }
    }
}
