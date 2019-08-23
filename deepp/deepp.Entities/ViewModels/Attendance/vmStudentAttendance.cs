using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.Attendance
{
 public   class vmStudentAttendance
    {
        public int StudentAttendanceDeatilId { get; set; }
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public int StudentAttendanceId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int AttendanceTypeId { get; set; }
        public string Attendancetypename { get; set; }
        public string ColourName { get; set; }
        public string ColorCode { get; set; }
        public string AttendanceDate { get; set; }

        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }

        public int TotalCount { get; set; }
        public int PresentPercentage { get; set; }
        public int AbsentPercentage { get; set; }
         public string InTime { get; set; }
        public string OutTime { get; set; }


        //  public int 





        //    STUAtt.[InstituteId]
        //,Ins.Name as InstituteName
        //,STUAtt.Id
        //,[AcademicBranchId]
        //,[AttendanceDate]
        //,[PresentCount]
        //,[AbsentCount]
        //,[TotalCount]
        //,[PresentPercentage]
        //,[AbsentPercentage]
        //   ,STUAtt.Id as UserAttendanceId
        //   ,STUAttdet.AttendanceTypeId
        //,STUAttdet.StudentId
        //,UInfo.Name as StudentName

        //      ,At.Name as Attendancetypename
        //,'' as InTime
        //,'' as OutTime
        //,STUAttdet.Comments

        //public DateTime Intime { get; set; }
        //public DateTime Outtime { get; set; }
        //public int TeacherId { get; set; }
        //public int AcademicSessionId { get; set; }
        //public int AcademicBranchId { get; set; }
        //public int AcademicClassId { get; set; }
        //public Nullable<int> AcademicShiftId { get; set; }
        //public Nullable<int> AcademicSectionId { get; set; }
        //public Nullable<int> AcamedicGroupId { get; set; }
        //public Nullable<int> SubjectAcademicClassMappingsId { get; set; }
        //public Nullable<int> AcademicPeriodId { get; set; }
        //public DateTime? LastUpdateTime { get; set; }
        //public Nullable<System.DateTime> SyncDate { get; set; }
        //public Nullable<int> PresentCount { get; set; }
        //public Nullable<int> AbsentCount { get; set; }
        //public Nullable<int> TotalCount { get; set; }
        //public Nullable<int> AbscondingCount { get; set; }
        //public Nullable<decimal> PresentPercentage { get; set; }
        //public Nullable<decimal> AbsentPercentage { get; set; }
        //public Nullable<decimal> AbscondingPercentage { get; set; }
        //public Nullable<int> LocalId { get; set; }     

        //public Nullable<bool> IsAbsconding { get; set; }
        //public string PeriodName { get; set; }


    }
    public class vmStudentAttendancelist
    {
        public List<vmStudentAttendance> vmStudentAttendance { get; set; }
    }
}
