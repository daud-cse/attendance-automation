using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Employee: Entity
    {
        public Employee()
        {
            this.AcademicClassSectionMappings = new List<AcademicClassSectionMapping>();
            this.AcademicClassSectionMappings1 = new List<AcademicClassSectionMapping>();
            this.Diaries = new List<Diary>();
            this.RoutineDetails = new List<RoutineDetail>();
            this.RoutineDetails1 = new List<RoutineDetail>();
            this.TeacherSubjectAcademicMappings = new List<TeacherSubjectAcademicMapping>();
        }

        public int EmployeeId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public Nullable<int> MaritalStatusId { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings { get; set; }
        public virtual ICollection<AcademicClassSectionMapping> AcademicClassSectionMappings1 { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails { get; set; }
        public virtual ICollection<RoutineDetail> RoutineDetails1 { get; set; }
        public virtual ICollection<TeacherSubjectAcademicMapping> TeacherSubjectAcademicMappings { get; set; }
    }
}
