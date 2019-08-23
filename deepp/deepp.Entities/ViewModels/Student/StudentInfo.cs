using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.Student
{
    public class StudentInfo
    {
        public int InstituteId { get; set; }
        public int CurrentSessionId { get; set; }
        public string CurrentSessionName { get; set; }
        public int CurrentBranchId { get; set; }

        public string CurrentBranchName { get; set; }


        public int CurrentClassId { get; set; }

        public string CurrentClassName { get; set; }
        public int CurrentShiftId { get; set; }
        public string CurrentShiftName { get; set; }       
        public int CurrentSectionId { get; set; }
        public string CurrentSectionName { get; set; }
        public int StudentId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string StudentName { get; set; }

        public string CurrentRollNo { get; set; }
        public string ContactNumber1 { get; set; }

        public string ContactNumber2 { get; set; }
        public string DOB { get; set; }
        public string EmailAddress { get; set; }
        public int GenderId { get; set; }

        public string GenderName { get; set; }
        public int BloodGroupid { get; set; }
        public string BloodGroupName { get; set; }

        public int RelegionId { get; set; }
        public string ReligionName { get; set; }
    }
}
