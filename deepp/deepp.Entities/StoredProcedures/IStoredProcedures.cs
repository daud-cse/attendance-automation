using deepp.Entities.StoredProcedures.Models;
using deepp.Entities.ViewModels;
using deepp.Entities.ViewModels.Attendance;
using deepp.Entities.ViewModels.DashBoard;
using deepp.Entities.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Data;

namespace deepp.Entities.Models
{
    public interface IStoredProcedures
    {
        IEnumerable<SprPartners_Result> SprPartnersComplex(IEnumerable<TTProduct> products, Guid userId);
        IEnumerable<SprPartners_Result> SprPartners(Guid userId, ref object userName);
        IEnumerable<string> SprGetRights(int userId);
        string SprSmsGeneration();
        bool SprInstituteDefaultSetup(int instituteId);
        string SprGetPIN(int userId);
        VmUserInfoDetails GetUserDetails(int InstituteId, string UserId, string Password);
        List<VmUserInfo> spUserInfoSearch(int InstituteId, int UserInfoTypeId, string SearchItem);

        List<VmUserInfo> GetUserInfo(int InstituteId, int SessionId, int UserInfoTypeId, string SearchItem);        
        DataSet GetStudentReports(int instituteId, int? StudentId, string ClassId, string SectionId);
        Dashboard GetMaleFemaleRation(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int GenderType, int InstituteId, int BrancheId);
        Dashboard GetGlobalDashBoard(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int userType, int InstituteId, int BrancheId);
        IEnumerable<Test> test();
        string ExamProcess(int InstituteId, int SessionId, int ClassId, int ExamTypeId, int userid);

        VMInstituteDashBoard GetInstituteDashBoards(int InstituteId, int CurrentSessionId);
        AttendanceConfigurationDetail GetAttendanceDataSynInfoboth(int InstituteId, int UserTypeId, string MachineSerialNo);
        List<StudentInfo> GetStudentList(int InstituteId, int CurrentSessionId, int AcademicBranchId, int AcademicClassesId, int AcademicShiftId, int AcademicSectionId);


        VmAttendanceDataSynInfo GetAttendanceDataSynInfo(int InstituteId,int UserTypeId,string MachineSerialNo);
        
        StatusSP AttendanceDataMigration(int InstituteId, string deviceInfo, int userId);
        vmAttendanceDataProcessInfo AttendanceDataProcessInfo(int InstituteId, int userId);
        List<vmStudentAttendance> spStudentAttendance(int InstituteId, int StudentId, int AcademicSessionId, int Year, int Month, int Day);
       
        }
}
