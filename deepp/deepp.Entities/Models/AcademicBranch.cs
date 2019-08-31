using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class AcademicBranch: Entity
    {
        public AcademicBranch()
        {
            this.AcademicBranchesOfUserInfoes = new List<AcademicBranchesOfUserInfo>();
            this.AcademicCalendars = new List<AcademicCalendar>();
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.AdmissionForms = new List<AdmissionForm>();
            this.LeaveApplications = new List<LeaveApplication>();
            this.StudentAttendances = new List<StudentAttendance>();
            this.Students = new List<Student>();
            this.Teachers = new List<Teacher>();
            this.UserAttendances = new List<UserAttendance>();
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
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<UserAttendance> UserAttendances { get; set; }
    }
}
