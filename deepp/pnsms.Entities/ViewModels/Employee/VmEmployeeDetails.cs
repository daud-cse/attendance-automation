using System;

namespace pnsms.Entities.ViewModels.Employee
{
    public class VmEmployeeDetails
    {
        public int EmployeeId { get; set; } 
        public string BranchName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; } 
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int InstituteId { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}
