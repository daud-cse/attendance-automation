using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ExamTypeWiseTabulationSheetDetail: Entity
    {
        public int Id { get; set; }
        public int ExamTypeId { get; set; }
        public int InstituteId { get; set; }
        public Nullable<int> AcademicClassesId { get; set; }
        public string AcademicClassName { get; set; }
        public Nullable<int> SubjectAcademicClassMappingsMapId { get; set; }
        public int SubjectMarks { get; set; }
        public Nullable<int> AcademicGroupId { get; set; }
        public string AcademicGroupName { get; set; }
        public Nullable<int> AcademicClassesSectionMapId { get; set; }
        public Nullable<int> AcademicSectionId { get; set; }
        public string AcademicSectionName { get; set; }
        public Nullable<int> AcademicSessionId { get; set; }
        public string AcademicSessionName { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<int> InstituteSubjectClassId { get; set; }
        public string InstituteSubjectName { get; set; }
        public decimal TotalMarks { get; set; }
        public Nullable<decimal> PercentageTotalMarks { get; set; }
        public decimal AverageMarks { get; set; }
        public decimal AcceptTotalMarks { get; set; }
        public Nullable<int> ExamGradeId { get; set; }
        public string ExamGradeName { get; set; }
        public Nullable<decimal> ExamGradePoint { get; set; }
        public virtual ExamType ExamType { get; set; }
    }
}
