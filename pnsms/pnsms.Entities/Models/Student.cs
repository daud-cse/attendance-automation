using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Student: Entity
    {
        public Student()
        {
            this.CoCurricularActivitiesOfStudents = new List<CoCurricularActivitiesOfStudent>();
            this.ExamSubjectMarks = new List<ExamSubjectMark>();
            this.FeesCollections = new List<FeesCollection>();
            this.FeesGenerateStudents = new List<FeesGenerateStudent>();
            this.GuardiansOfStudents = new List<GuardiansOfStudent>();
            this.MobilePayments = new List<MobilePayment>();
            this.ScholarshipOfStudents = new List<ScholarshipOfStudent>();
            this.ShortMessageDetails = new List<ShortMessageDetail>();
            this.Siblings = new List<Sibling>();
            this.Siblings1 = new List<Sibling>();
            this.StudentAttendanceDetails = new List<StudentAttendanceDetail>();
            this.SubjectStudentMappings = new List<SubjectStudentMapping>();
        }

        public int StudentId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public Nullable<int> CurrentAcademicSessionId { get; set; }
        public Nullable<int> CurrentAcademicBranchId { get; set; }
        public Nullable<int> CurrentAcademicClassId { get; set; }
        public Nullable<int> CurrentAcademicShiftId { get; set; }
        public Nullable<int> CurrentAcademicSectionId { get; set; }
        public Nullable<int> CurrentAcademicVerssionId { get; set; }
        public Nullable<int> CurrentAcademicGroupId { get; set; }
        public string CurrentRollNo { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<bool> IsCurrent { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicClassSectionMapping AcademicClassSectionMapping { get; set; }
        public virtual AcademicGroup AcademicGroup { get; set; }
        public virtual AcademicSession AcademicSession { get; set; }
        public virtual AcademicShift AcademicShift { get; set; }
        public virtual AcademicVersion AcademicVersion { get; set; }
        public virtual ICollection<CoCurricularActivitiesOfStudent> CoCurricularActivitiesOfStudents { get; set; }
        public virtual ICollection<ExamSubjectMark> ExamSubjectMarks { get; set; }
        public virtual FeesCollectionAdvance FeesCollectionAdvance { get; set; }
        public virtual ICollection<FeesCollection> FeesCollections { get; set; }
        public virtual ICollection<FeesGenerateStudent> FeesGenerateStudents { get; set; }
        public virtual ICollection<GuardiansOfStudent> GuardiansOfStudents { get; set; }
        public virtual ICollection<MobilePayment> MobilePayments { get; set; }
        public virtual ICollection<ScholarshipOfStudent> ScholarshipOfStudents { get; set; }
        public virtual ICollection<ShortMessageDetail> ShortMessageDetails { get; set; }
        public virtual ICollection<Sibling> Siblings { get; set; }
        public virtual ICollection<Sibling> Siblings1 { get; set; }
        public virtual ICollection<StudentAttendanceDetail> StudentAttendanceDetails { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<SubjectStudentMapping> SubjectStudentMappings { get; set; }
    }
}
