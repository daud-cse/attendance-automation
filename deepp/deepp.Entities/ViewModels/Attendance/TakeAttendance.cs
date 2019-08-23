using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.Attendance
{
   public class TakeAttendance
    {

        public int Id { get; set; }
        public int AcademicBranchId { get; set; }
        public string AcademicBranchName { get; set; }
        public int InstituteId { get; set; }

        public int SubjectAcademicClassMappingsId { get; set; }
       public DateTime Attendancedate { get; set; }

       public bool IsTakenAttendance{get;set;}
         public int AcademicClassId { get; set; }
        public string AcademicClassName { get; set; }
        public int AcademicClassSectionMapId { get; set; }
        public string AcademicSectionName { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TeacherId { get; set; }
     
       public string TeacherName { get; set; }
        public int AcademicSessionId { get; set; }

       public int AcademicSessionName { get; set; }
        public int AcademicGroupName { get; set; }
      
    }
}
