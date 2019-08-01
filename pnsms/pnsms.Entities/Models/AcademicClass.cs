using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AcademicClass: Entity
    {
        public AcademicClass()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.AdmissionForms = new List<AdmissionForm>();
            this.Exams = new List<Exam>();
            this.ExamProcesses = new List<ExamProcess>();
            this.FeesAcademicClasses = new List<FeesAcademicClass>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.InstituteSubjectClasses = new List<InstituteSubjectClass>();
            this.Routines = new List<Routine>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
            this.SubjectAcademicClassMappingSubjectTypes = new List<SubjectAcademicClassMappingSubjectType>();
            this.Teachers = new List<Teacher>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual ICollection<AdmissionForm> AdmissionForms { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual ICollection<FeesAcademicClass> FeesAcademicClasses { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<InstituteSubjectClass> InstituteSubjectClasses { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public virtual ICollection<SubjectAcademicClassMappingSubjectType> SubjectAcademicClassMappingSubjectTypes { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
    }
}