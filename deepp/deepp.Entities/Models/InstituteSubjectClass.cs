using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class InstituteSubjectClass: Entity
    {
        public InstituteSubjectClass()
        {
            this.ExamSubjects = new List<ExamSubject>();
        }

        public int Id { get; set; }
        public int InstituteSubjectId { get; set; }
        public int ClassId { get; set; }
        public bool IsActive { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual ICollection<ExamSubject> ExamSubjects { get; set; }
        public virtual InstituteSubject InstituteSubject { get; set; }
    }
}
