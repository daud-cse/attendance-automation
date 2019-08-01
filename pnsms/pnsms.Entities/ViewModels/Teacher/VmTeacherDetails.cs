using System;

namespace pnsms.Entities.ViewModels.Teacher
{
    public class VmTeacherDetails
    {
        public int TeacherId { get; set; } 
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; } 
        public string AcademicBranch { get; set; }
        public string AcademicClass { get; set; }
        public string AcademicSection { get; set; }
        public string MaritalStatus { get; set; }
        public string Designation { get; set; }
        public int? DesignationOrdering { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}
