using Dapper;
using pnsms.Entities.StoredProcedures.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
//using Dapper;
using pnsms.Entities.ViewModels.DashBoard;
using pnsms.Entities.ViewModels.Exams;
using System.Configuration;
using pnsms.Entities.ViewModels.Student;
using pnsms.Entities.ViewModels.Attendance;
using pnsms.Entities.ViewModels.Fees;

namespace pnsms.Entities.Models
{
    public partial class PNSMSContext : IStoredProcedures
    {

        public IEnumerable<SprPartners_Result> SprPartnersComplex(IEnumerable<TTProduct> products, Guid userId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));

            foreach (var v in products)
            {
                dt.Rows.Add(v.Id, v.Name);
            }

            var prdLst = new SqlParameter("@ProductList", SqlDbType.Structured);
            prdLst.Value = dt;
            prdLst.TypeName = "dbo.TTProducts";

            var user = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            user.Value = userId;

            return Database.SqlQuery<SprPartners_Result>("SprPartnersComplex @ProductList, @UserId", prdLst, user);
        }

        public IEnumerable<SprPartners_Result> SprPartners(Guid userId, ref object userName)
        {
            var user = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            user.Value = userId;

            var name = new SqlParameter("@Name", SqlDbType.NVarChar, 512);
            name.Direction = ParameterDirection.InputOutput;
            name.Value = "";
            userName = name;

            return Database.SqlQuery<SprPartners_Result>("SprPartners  @UserId, @Name out", user, name);

        }

        public IEnumerable<string> SprGetRights(int userId)
        {
            var user = new SqlParameter("@UserInfoId", SqlDbType.Int);
            user.Value = userId;

            return Database.SqlQuery<string>("SprGetRights  @UserInfoId", user);
        }

        public string SprSmsGeneration()
        {
            var status = new SqlParameter("@Status", SqlDbType.NVarChar, 50);
            status.Direction = ParameterDirection.InputOutput;
            status.Value = "";

            Database.ExecuteSqlCommand("SprSmsGeneration @Status out", status);

            return (string)status.Value;
        }

        public bool SprInstituteDefaultSetup(int instituteId)
        {
            var instituteId_ = new SqlParameter("@InstituteId", SqlDbType.Int);
            instituteId_.Value = instituteId;

            var isSuccess = new SqlParameter("@IsSuccess", SqlDbType.Bit);
            isSuccess.Direction = ParameterDirection.InputOutput;
            isSuccess.Value = false;

            Database.ExecuteSqlCommand("SprInstituteDefaultSetup @InstituteId @IsSuccess out", instituteId_, isSuccess);

            return (bool)isSuccess.Value;
        }

        public string SprGetPIN(int userId)
        {
            var userId_ = new SqlParameter("@UserInfoId", SqlDbType.Int);
            userId_.Value = userId;

            var pin = new SqlParameter("@PIN", SqlDbType.NVarChar, 32);
            pin.Direction = ParameterDirection.InputOutput;
            pin.Value = "";

            Database.ExecuteSqlCommand("SprGetPIN @UserInfoId @PIN out", userId_, pin);

            return (string)pin.Value;


        }
        public DataSet GetStudentReports(int instituteId, int? StudentId, string ClassId, string SectionId)
        {
            PNSMSContext _contex = new PNSMSContext();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = _contex.Database.Connection.ConnectionString;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "[PNSMS].[dbo].[SprStudentsReports] " + instituteId + "," + "'" + StudentId + "'" + "," + "'" + ClassId + "'" + "," + "'" + SectionId + "'";
            DataSet ds = new DataSet();
            System.Data.SqlClient.SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;
            ad.Fill(ds);
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return ds;
        }
        public VmUserInfoDetails GetUserDetails(int InstituteId, string UserId, string Password)
        {


            VmUserInfoDetails objVmUserInfoDetails = new VmUserInfoDetails();
            List<UserInfoDetails> UserInfoDetails = new List<UserInfoDetails>();
            objVmUserInfoDetails.RightsDetails = new List<RightsDetails>();
            objVmUserInfoDetails.objUserInfoDetails = new UserInfoDetails();

            using (var multi = Database.Connection.QueryMultiple("[sprGetUserDetails ]" + InstituteId + "," + "'" + UserId + "'" + "," + "'" + Password + "'"))
            {
                UserInfoDetails = multi.Read<UserInfoDetails>().AsList();

                UserInfoDetails.ForEach(delegate (UserInfoDetails item)
                {
                    objVmUserInfoDetails.objUserInfoDetails = item;
                });

                objVmUserInfoDetails.RightsDetails = multi.Read<RightsDetails>().AsList();

                if (UserInfoDetails.Count == 0)
                {
                    objVmUserInfoDetails.objUserInfoDetails = null;
                }
                if (objVmUserInfoDetails.RightsDetails.Count == 0)
                {
                    objVmUserInfoDetails.RightsDetails = null;
                }
            }

            return objVmUserInfoDetails;
        }
        public List<VmUserInfo> GetUserInfo(int InstituteId, int SessionId, int UserInfoTypeId, string SearchItem)
        {
            List<VmUserInfo> lstVmUserInfo = new List<VmUserInfo>();
            using (var multi = Database.Connection.QueryMultiple("[spUserInfoSearch] " + InstituteId + "," + SessionId + "," + UserInfoTypeId + "," + "'" + SearchItem + "'"))
            {
                lstVmUserInfo = multi.Read<VmUserInfo>().AsList();
            }

            return lstVmUserInfo;
        }

        public VmAttendanceDataSynInfo GetAttendanceDataSynInfo(int InstituteId, int UserTypeId, string MachineSerialNo)
        {

            VmAttendanceDataSynInfo objVmAttendanceDataSynInfo = new VmAttendanceDataSynInfo();
            // objVmAttendanceDataSynInfo.lstAttendanceDataSynInfo = new List<AttendanceDataSynInfo>();
            var lstAttendanceDataSynInfo1 = new List<AttendanceDataSynInfo>();
            // objVmAttendanceDataSynInfo.lstAttendanceType = new List<AttendanceType>();
            using (var multi = Database.Connection.QueryMultiple("[sprGetAttendanceDataSynInfo]" + InstituteId + "," + "'" + UserTypeId + "'" + "," + "'" + MachineSerialNo + "'"))
            {
                //lstAttendanceDataSynInfo1 = multi.Read<AttendanceDataSynInfo>().AsList();
                //objVmAttendanceDataSynInfo.lstAttendanceType = multi.Read<AttendanceType>().AsList();
                objVmAttendanceDataSynInfo.lstLastAttendanceSynDate = multi.Read<VmAttendanceDataSynInfo>().AsList();
            }
            //    objVmAttendanceDataSynInfo.lstAttendanceDataSynInfo= lstAttendanceDataSynInfo1;
            return objVmAttendanceDataSynInfo;
        }
        public List<VmUserInfo> spUserInfoSearch(int InstituteId, int UserInfoTypeId, string SearchItem)
        {

            List<VmUserInfo> lstVmUserInfo = new List<VmUserInfo>();
            using (var multi = Database.Connection.QueryMultiple("[spUserInfoSearch]" + InstituteId + "," + "'" + UserInfoTypeId + "'" + "," + "'" + SearchItem + "'"))
            {
                lstVmUserInfo = multi.Read<VmUserInfo>().AsList();
            }
            return lstVmUserInfo;
        }

        #region Student Attendance
        public List<vmStudentAttendance> spStudentAttendance(int InstituteId, int StudentId, int AcademicSessionId, int Year, int Month, int Day)
        {
            List<vmStudentAttendance> lstvmStudentAttendance = new List<vmStudentAttendance>();
            try
            {

                using (var multi = Database.Connection.QueryMultiple("[SprStudentAttendanceReports]" + InstituteId + "," + "'" + StudentId + "'" + "," + "'" + AcademicSessionId + "'" + "," + "'" + Year + "'" + "," + "'" + Month + "'" + "," + "'" + Day + "'"))
                {
                    lstvmStudentAttendance = multi.Read<vmStudentAttendance>().AsList();
                }
                return lstvmStudentAttendance;
            }
            catch (Exception ex)
            {
                return lstvmStudentAttendance;
            }

        }
        #endregion

        #region Fees Collection
        public List<VmStudentFeesCollection> spStudentFeesCollection(int InstituteId, int StudentId, int AcademicSessionId, int Year, int Month, int Day)
        {
            List<VmStudentFeesCollection> lstvmVmStudentFeesCollection = new List<VmStudentFeesCollection>();
            try
            {

                using (var multi = Database.Connection.QueryMultiple("[SprStudentFeesCollectionReports]" + InstituteId + "," + "'" + StudentId + "'" + "," + "'" + AcademicSessionId + "'" + "," + "'" + Year + "'" + "," + "'" + Month + "'" + "," + "'" + Day + "'"))
                {
                    lstvmVmStudentFeesCollection = multi.Read<VmStudentFeesCollection>().AsList();
                }
                return lstvmVmStudentFeesCollection;
            }
            catch (Exception ex)
            {
                return lstvmVmStudentFeesCollection;
            }

        }
        #endregion
        public VmExamTabulationSheet GetVmExamTabulationSheet(int instituteid, int examtypeId, int studentId)
        {
            VmExamTabulationSheet objVmExamTabulationSheet = new VmExamTabulationSheet();
            objVmExamTabulationSheet.objlstExamTypeWiseTabulationSheetMaster = new List<ExamTypeWiseTabulationSheetMaster>();
            objVmExamTabulationSheet.lstExamTypeWiseTabulationSheetDetails = new List<ExamTypeWiseTabulationSheetDetail>();

            using (var multi = Database.Connection.QueryMultiple("[sprGetVmExamTabulationSheet ]" + instituteid + "," + "'" + examtypeId + "'" + "," + "'" + studentId + "'"))
            {
                objVmExamTabulationSheet.objlstExamTypeWiseTabulationSheetMaster = multi.Read<ExamTypeWiseTabulationSheetMaster>().AsList();

                objVmExamTabulationSheet.lstExamTypeWiseTabulationSheetDetails = multi.Read<ExamTypeWiseTabulationSheetDetail>().AsList();
            }

            return objVmExamTabulationSheet;
        }
        public Dashboard GetMaleFemaleRation(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int GenderType, int InstituteId, int BrancheId)
        {

            IEnumerable<MaleFemaleRatio> maleFemaleRation;
            List<cRatio> newcratio = new List<cRatio>();
            Dashboard dashboard = new Dashboard();
            dashboard.MaleFemaleRation = newcratio;
            using (var multi = Database.Connection.QueryMultiple("[SprMaleFemaleRatio]" + CountryId + "," + DivisionId + "," + DistrictId + "," + UpaThanaId + "," + UnionId + "," + GenderType + "," + InstituteId + "," + BrancheId))
            {
                maleFemaleRation = multi.Read<MaleFemaleRatio>().AsList();
                dashboard.DashboardHeader = multi.Read<DashboardHeader>();
            }
            maleFemaleRation.AsList().ForEach(delegate (MaleFemaleRatio item)
            {

                cRatio cratio = new cRatio();
                cratio.Name = item.InstituteName;
                cratio.Count = item.Total;
                cratio.Propertices = new List<KeyValuePair<string, decimal>>();
                cratio.Propertices.Add(new KeyValuePair<string, decimal>("Male", item.Male));
                cratio.Propertices.Add(new KeyValuePair<string, decimal>("FeMale", item.Female));
                dashboard.MaleFemaleRation.AsList().Add(cratio);

            });
            return dashboard;
        }
        public Dashboard GetGlobalDashBoard(int CountryId, int DivisionId, int DistrictId, int UpaThanaId, int UnionId, int userType, int InstituteId, int BrancheId)
        {

            List<MaleFemaleRatioGlobal> maleFemaleRation;
            List<cRatio> newcratio = new List<cRatio>();
            List<InstituteTotalInfo> lstInstituteTotalInfo = new List<InstituteTotalInfo>();
            Dashboard dashboard = new Dashboard();
            dashboard.MaleFemaleRation = newcratio;
            using (var multi = Database.Connection.QueryMultiple("[SpGlobalDashBoard]" + CountryId + "," + DivisionId + "," + DistrictId + "," + UpaThanaId + "," + UnionId + "," + userType + "," + InstituteId + "," + BrancheId))
            {

                dashboard.lstInstitute = multi.Read<VmInstitute>().AsList();
                dashboard.lstInstituteTotalInfo = multi.Read<InstituteTotalInfo>().AsList();
                dashboard.lstMaleFemaleRatioGlobal = multi.Read<MaleFemaleRatioGlobal>().AsList();
                dashboard.lstTeacherMaleFemaleRatioGlobal = multi.Read<MaleFemaleRatioGlobal>().AsList();
                dashboard.lstHeadMaster = multi.Read<GlobalHeadmasterList>().AsList();


            }

            return dashboard;
        }
        public IEnumerable<Test> test()
        {
            Test Test = new Test();
            var conn = new SqlConnection();

            var sql = "test"; // Stored Procedure Name  
            PNSMSContext _contex = new PNSMSContext();
            SqlConnection con = new SqlConnection();

            con.ConnectionString = _contex.Database.Connection.ConnectionString;

            using (var multi = Database.Connection.QueryMultiple("test", commandType: CommandType.StoredProcedure))
            {
                var AddressType = multi.Read<AddressType>();
                var Department = multi.Read<Department>();
            }
            var abc = Database.SqlQuery<Test>("test");



            return abc;


        }
        public string ExamProcess(int InstituteId, int SessionId, int ClassId, int ExamTypeId, int userid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PNSMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExamTypeWiseProcess";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par = new SqlParameter("@InstituteId", SqlDbType.Int);
            cmd.Parameters.Add(par).Value = InstituteId;

            SqlParameter par1 = new SqlParameter("@SessionId", SqlDbType.Int);
            cmd.Parameters.Add(par1).Value = SessionId;

            SqlParameter par2 = new SqlParameter("@ClassId", SqlDbType.Int);
            cmd.Parameters.Add(par2).Value = ClassId;

            SqlParameter par3 = new SqlParameter("@ExamTypeId", SqlDbType.Int);
            cmd.Parameters.Add(par3).Value = ExamTypeId;

            SqlParameter parOut = new SqlParameter("@Msg", SqlDbType.VarChar, 2000);
            parOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parOut);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            string Msg = "";
            Msg = parOut.Value.ToString();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return Msg;

        }
        public VMInstituteDashBoard GetInstituteDashBoards(int InstituteId, int CurrentSessionId)
        {


            VMInstituteDashBoard objVMInstituteDashBoard = new VMInstituteDashBoard();

            objVMInstituteDashBoard.objInstituteTotalInfo = new InstituteTotalInfo();

            objVMInstituteDashBoard.lstClassWiseStudent = new List<ClassWiseStudent>();
            objVMInstituteDashBoard.lstAttendanceSummary = new List<AttendanceSummary>();

            objVMInstituteDashBoard.lstMaleFemaleRatio = new List<MaleFemaleRatioInstituteWise>();

            using (var multi = Database.Connection.QueryMultiple("[spInstituteDashBoard]" + InstituteId + "," + CurrentSessionId))
            {
                var lstInstituteTotalInfo = multi.Read<InstituteTotalInfo>().AsList();

                lstInstituteTotalInfo.ForEach(delegate (InstituteTotalInfo item)
                {
                    objVMInstituteDashBoard.objInstituteTotalInfo = item;
                });
                objVMInstituteDashBoard.lstClassWiseStudent = multi.Read<ClassWiseStudent>().AsList();

                objVMInstituteDashBoard.lstAttendanceSummary = multi.Read<AttendanceSummary>().AsList();

                objVMInstituteDashBoard.lstMaleFemaleRatio = multi.Read<MaleFemaleRatioInstituteWise>().AsList();

            }

            return objVMInstituteDashBoard;
        }
        public List<StudentInfo> GetStudentList(int InstituteId, int CurrentSessionId, int AcademicBranchId, int AcademicClassesId, int AcademicShiftId, int AcademicSectionId)
        {

            List<StudentInfo> lstStudentInfo = new List<StudentInfo>();
            using (var multi = Database.Connection.QueryMultiple("[StudentList]" + InstituteId + "," + CurrentSessionId + "," + AcademicBranchId + "," + AcademicClassesId + "," + AcademicShiftId + "," + AcademicSectionId))
            {
                lstStudentInfo = multi.Read<StudentInfo>().AsList();


            }

            return lstStudentInfo;
        }


    }
}
