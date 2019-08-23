using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicSession: Entity
    {
        public AcademicSession()
        {
            this.AcademicCalendars = new List<AcademicCalendar>();
            this.AdmissionForms = new List<AdmissionForm>();
            this.ExamProcesses = new List<ExamProcess>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.ResultPublications = new List<ResultPublication>();
            this.Routines = new List<Routine>();
            this.ScholarshipOfStudents = new List<ScholarshipOfStudent>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
            this.SubjectStudentMappings = new List<SubjectStudentMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public System.DateTime StartAt { get; set; }
        public Nullable<System.DateTime> EndAt { get; set; }
        public string Remarks { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRunning { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<AcademicCalendar> AcademicCalendars { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<AdmissionForm> AdmissionForms { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<ResultPublication> ResultPublications { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<ScholarshipOfStudent> ScholarshipOfStudents { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public virtual ICollection<SubjectStudentMapping> SubjectStudentMappings { get; set; }
    }
}
