using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class AcademicBranch: Entity
    {
        public AcademicBranch()
        {
            this.AcademicBranchesOfUserInfoes = new List<AcademicBranchesOfUserInfo>();
            this.AcademicCalendars = new List<AcademicCalendar>();
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.AdmissionForms = new List<AdmissionForm>();
            this.Exams = new List<Exam>();
            this.ExamProcesses = new List<ExamProcess>();
            this.FeesBooths = new List<FeesBooth>();
            this.FeesGenerateAcademics = new List<FeesGenerateAcademic>();
            this.LeaveApplications = new List<LeaveApplication>();
            this.RoutinePeriods = new List<RoutinePeriod>();
            this.Routines = new List<Routine>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.SubjectAcademicClassMappings = new List<SubjectAcademicClassMapping>();
            this.Teachers = new List<Teacher>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
            this.UserAttendances = new List<UserAttendance>();
            this.Vouchers = new List<Voucher>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string WelComeText { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> TagId { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<AcademicBranchesOfUserInfo> AcademicBranchesOfUserInfoes { get; set; }
        public virtual ICollection<AcademicCalendar> AcademicCalendars { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual ICollection<AdmissionForm> AdmissionForms { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ExamProcess> ExamProcesses { get; set; }
        public virtual ICollection<FeesBooth> FeesBooths { get; set; }
        public virtual ICollection<FeesGenerateAcademic> FeesGenerateAcademics { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<RoutinePeriod> RoutinePeriods { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SubjectAcademicClassMapping> SubjectAcademicClassMappings { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
        public virtual ICollection<UserAttendance> UserAttendances { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
