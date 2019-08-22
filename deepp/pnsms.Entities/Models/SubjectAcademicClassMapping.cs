using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class SubjectAcademicClassMapping: Entity
    {
        public SubjectAcademicClassMapping()
        {
            this.ExamSubjectMarks = new List<ExamSubjectMark>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.SubjectAcademicClassMappingSubjectTypes = new List<SubjectAcademicClassMappingSubjectType>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int AcademicBranchId { get; set; }
        public int AcademicClassId { get; set; }
        public int AcademicClassSectionMapId { get; set; }
        public int SubjectMarks { get; set; }
        public int SubjectId { get; set; }
        public Nullable<int> ParentSubjectId { get; set; }
        public int TeacherId { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> MarksEntryTypeKey { get; set; }
        public Nullable<bool> IsSubjectGroupWise { get; set; }
        public Nullable<int> SubjectGroupId { get; set; }
        public string SubjectGroupNameList { get; set; }
        public int AcademicSessionId { get; set; }
        public Nullable<int> AcademicGroupId { get; set; }
        public int SubjectTypeId { get; set; }
        public Nullable<System.DateTime> ExamDate { get; set; }
        public bool IsFailApplicable { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicClassSectionMapping AcademicClassSectionMapping { get; set; }
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual ICollection<ExamSubjectMark> ExamSubjectMarks { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual InstituteSubject InstituteSubject { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }
        public virtual SubjectType SubjectType { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<SubjectAcademicClassMappingSubjectType> SubjectAcademicClassMappingSubjectTypes { get; set; }
    }
}
