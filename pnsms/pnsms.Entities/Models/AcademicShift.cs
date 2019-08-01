using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AcademicShift: Entity
    {
        public AcademicShift()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.ExamProcesses = new List<ExamProcess>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.RoutinePeriods = new List<RoutinePeriod>();
            this.Routines = new List<Routine>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public Nullable<System.TimeSpan> StartAt { get; set; }
        public Nullable<System.TimeSpan> EndAt { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<RoutinePeriod> RoutinePeriods { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
    }
}
