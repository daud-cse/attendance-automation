using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Subject: Entity
    {
        public Subject()
        {
            this.Diaries = new List<Diary>();
            this.InstituteSubjects = new List<InstituteSubject>();
            this.RoutineDetails = new List<RoutineDetail>();
            this.Subject1 = new List<Subject>();
            this.SubjectAcademicClassMappingSubjectTypes = new List<SubjectAcademicClassMappingSubjectType>();
            this.SubjectStudentMappings = new List<SubjectStudentMapping>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentSubjectId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> TagId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string Code { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public string ShortName { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
        public virtual ICollection<InstituteSubject> InstituteSubjects { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails { get; set; }
        public virtual ICollection<Subject> Subject1 { get; set; }
        public virtual Subject Subject2 { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<SubjectAcademicClassMappingSubjectType> SubjectAcademicClassMappingSubjectTypes { get; set; }
        public virtual ICollection<SubjectStudentMapping> SubjectStudentMappings { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
    }
}
