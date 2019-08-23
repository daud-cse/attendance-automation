using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Exam: Entity
    {
        public Exam()
        {
            this.ExamSubjects = new List<ExamSubject>();
            this.ExamSubjectMarks = new List<ExamSubjectMark>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public int AcademicBranchId { get; set; }
        public int AcademicClassesId { get; set; }
        public Nullable<int> AcademicClassesSectionMapId { get; set; }
        public int ExamTypeId { get; set; }
        public bool IsGroupExam { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> ExamDateFrom { get; set; }
        public Nullable<System.DateTime> ExamDateTo { get; set; }
        public string ExamTime { get; set; }
        public Nullable<decimal> TotalMarks { get; set; }
        public Nullable<decimal> HighestMarks { get; set; }
        public Nullable<decimal> PassMarks { get; set; }
        public Nullable<decimal> AcceptMarks { get; set; }
        public System.DateTime LastUpdateTime { get; set; }
        public bool IsActive { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicClassSectionMapping AcademicClassSectionMapping { get; set; }
        public virtual ExamType ExamType { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<ExamSubject> ExamSubjects { get; set; }
        public virtual ICollection<ExamSubjectMark> ExamSubjectMarks { get; set; }
    }
}
