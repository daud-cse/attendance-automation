using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ExamSubjectMark: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int ExamId { get; set; }
        public int SubjectAcademicClassMappingsMapId { get; set; }
        public int StudentId { get; set; }
        public Nullable<decimal> MarksObtained { get; set; }
        public Nullable<decimal> AcceptMarksTotal { get; set; }
        public string Comment { get; set; }
        public System.DateTime LastUpdateTime { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual SubjectAcademicClassMapping SubjectAcademicClassMapping { get; set; }
        public virtual Student Student { get; set; }
    }
}
