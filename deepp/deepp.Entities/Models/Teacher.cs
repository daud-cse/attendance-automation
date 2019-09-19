using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Teacher: Entity
    {
        public Teacher()
        {
            this.StudentAttendances = new List<StudentAttendance>();
        }

        public int TeacherId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public Nullable<int> MaritalStatusId { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> CurrentAcademicBranchId { get; set; }
        public Nullable<int> DefaultAcademicClassId { get; set; }
        public Nullable<int> DefaultAcademicSectionId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string AboutTeacher { get; set; }
        public virtual AcademicBranch AcademicBranch { get; set; }
        public virtual AcademicClass AcademicClass { get; set; }
        public virtual AcademicClassSectionMapping AcademicClassSectionMapping { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
