using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ExamSubject: Entity
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int TecherId { get; set; }
        public int InstituteSubjectClassId { get; set; }
        public Nullable<decimal> TotalMarks { get; set; }
        public Nullable<decimal> PassMarks { get; set; }
        public Nullable<decimal> HighestMarks { get; set; }
        public Nullable<System.DateTime> ExamDate { get; set; }
        public string ExamTime { get; set; }
        public Nullable<int> TotalAttended { get; set; }
        public bool IsActive { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual InstituteSubjectClass InstituteSubjectClass { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
