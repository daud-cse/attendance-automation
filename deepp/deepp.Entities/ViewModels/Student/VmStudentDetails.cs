using System;

namespace deepp.Entities.ViewModels.Student
{
    public class VmStudentDetails
    {
        public int StudentId { get; set; }
        
        public string SessionName { get; set; }
        
        public string BranchName { get; set; }
        
        public string ClassName { get; set; }
        
        public string ShiftName { get; set; }
        
        public string SectionName { get; set; }
        
        public string VerssionName { get; set; }
        
        public string GroupName { get; set; }
        public string CurrentRollNo { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public int InstituteId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        
    }
}
