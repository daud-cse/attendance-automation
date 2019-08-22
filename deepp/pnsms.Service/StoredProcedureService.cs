using pnsms.Entities.Models;
using pnsms.Entities.StoredProcedures.Models;
using pnsms.Entities.ViewModels.DashBoard;
using pnsms.Entities.ViewModels.Exams;
using pnsms.Entities.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface IStoredProcedureService
    {
        IEnumerable<SprPartners_Result> SprPartnersComplex(IEnumerable<TTProduct> products, Guid userId);
        IEnumerable<SprPartners_Result> SprPartners(Guid userId, ref string userName);
        DataSet GetStudentReports(int instituteId, int? StudentId, string ClassId, string SectionId);
        Dashboard GetMaleFemaleRation(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int GenderType, int InstituteId, int BrancheId);
        Dashboard GetGlobalDashBoard(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int userType, int InstituteId, int BrancheId);
        IEnumerable<object> test();
        VmUserInfoDetails GetUserDetails(int InstituteId, string UserId, string Password);
        VmExamTabulationSheet GetVmExamTabulationSheet(int instituteid, int examtypeId, int studentId);
        string ExamProcess(int InstituteId, int SessionId, int ClassId, int ExamTypeId, int userid);
        List<VmUserInfo> GetUserInfo(int InstituteId, int SessionId, int UserInfoTypeId, string SearchItem);
        List<StudentInfo> GetStudentList(int InstituteId, int CurrentSessionId, int AcademicBranchId, int AcademicClassesId, int AcademicShiftId, int AcademicSectionId);
    }

    public class StoredProcedureService : IStoredProcedureService
    {
        private readonly IStoredProcedures storedProcedures;

        public StoredProcedureService(IStoredProcedures storedProcedures)
        {
            this.storedProcedures = storedProcedures;
        }
        public StoredProcedureService()
        {
            
        }
        public IEnumerable<SprPartners_Result> SprPartnersComplex(IEnumerable<TTProduct> products, Guid userId)
        {
            var result = storedProcedures.SprPartnersComplex(products, userId);
            result.Any();
            return result;
        }
        public IEnumerable<SprPartners_Result> SprPartners(Guid userId, ref string userName)
        {
            object name = "";

            var result = storedProcedures.SprPartners(userId, ref name);
            result.Any();

            userName = "" + ((SqlParameter)name).Value;

            return result;
        }
       public List<VmUserInfo> GetUserInfo(int InstituteId, int SessionId, int UserInfoTypeId, string SearchItem)
        {
           return  storedProcedures.GetUserInfo(InstituteId, SessionId, UserInfoTypeId, SearchItem);
        }
        public  DataSet GetStudentReports(int instituteId, int? StudentId, string ClassId, string SectionId)
        {
            return storedProcedures.GetStudentReports( instituteId,  StudentId,  ClassId,  SectionId);
        }
        public Dashboard GetMaleFemaleRation(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int GenderType, int InstituteId, int BrancheId)
        {
            return storedProcedures.GetMaleFemaleRation(CountryId, DivisionId, DistrictId, UpaThanaId, UnionId, GenderType, InstituteId, BrancheId);
        }
        public Dashboard GetGlobalDashBoard(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int userType, int InstituteId, int BrancheId)
        {
            return storedProcedures.GetGlobalDashBoard(CountryId, DivisionId, DistrictId, UpaThanaId, UnionId, userType, InstituteId, BrancheId);
        }
        public IEnumerable<object> test()
        {
            return storedProcedures.test();
        }
       public VmUserInfoDetails GetUserDetails(int InstituteId, string UserId, string Password)
        {
            storedProcedures.GetUserDetails( InstituteId,  UserId,  Password);
            return storedProcedures.GetUserDetails(InstituteId, UserId, Password); ;
        }
       public VmExamTabulationSheet GetVmExamTabulationSheet(int instituteid, int examtypeId, int studentId)
       {

           return storedProcedures.GetVmExamTabulationSheet(instituteid, examtypeId, studentId);
       }
       public string ExamProcess(int InstituteId, int SessionId, int ClassId, int ExamTypeId, int userid)
       {
           return storedProcedures.ExamProcess(InstituteId,SessionId,ClassId,ExamTypeId,userid);
       }
       public VMInstituteDashBoard GetInstituteDashBoards(int InstituteId, int CurrentSessionId)
       {
           return storedProcedures.GetInstituteDashBoards(InstituteId, CurrentSessionId);
       
       }
     public   List<StudentInfo> GetStudentList(int InstituteId, int CurrentSessionId, int AcademicBranchId, int AcademicClassesId, int AcademicShiftId, int AcademicSectionId)
       {
         return   storedProcedures.GetStudentList(InstituteId, CurrentSessionId, AcademicBranchId, AcademicClassesId, AcademicShiftId, AcademicSectionId);
       }
    }
}
