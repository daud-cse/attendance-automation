
using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.Attendance
{
    public class vmAttendanceDataProcessInfo
    {
        public List<AttendanceProcessDate> lstAttendanceProcessDateStudent { get; set; }
        public List<AttendanceProcessDate> lstAttendanceProcessDateTeacher { get; set; }
        public List<AttendanceConfigurationDetail> lstAttendanceConfigurationDetails { get; set; }
    }
    public class AttendanceProcessDate
    {
        public DateTime DateOnlyRecord { get; set; }
        public DateTime DateTimeRecord { get; set; }
        
        public int InstituteId { get; set; }
        public int CurrentAcademicBranchId { get; set; }
        public int AcademicSessionId { get; set; }
        public int CurrentAcademicClassId { get; set; }
        public int CurrentAcademicSectionId { get; set; }
        public int CountTotalStudent { get; set; }
        public string Name { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }

        public string SectionName { get; set; }
        public string deviceinfo { get; set; }
        public bool IsProcess { get; set; }

    }



}
