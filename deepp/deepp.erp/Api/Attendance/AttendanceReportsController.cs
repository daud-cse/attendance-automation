using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.Settings;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace deepp.erp.Api.Attendance
{
    public class AttendanceReportsController : ApiController
    {

        
          private readonly IDepartmentService _academicDepartmentService;
        private readonly IAcademicBranchService _academicBranchService;
        private readonly IAcademicClassService _academicClassService;
        private readonly IAcademicGroupService _academicGroupService;
        private readonly IAcademicSectionService _academicSectionService;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IAcademicPeriodService _academicPeriodService;
        private readonly IAcademicVersionService _academicVersionService;
        private readonly IAcademicShiftService _academicShiftService;
        private readonly IAcademicBranchesOfUserInfoService _academicBranchesOfUserInfoService;
        private readonly ICoCurricularActivityService _coCurricularActivityService;
        private readonly IScholarshipService _scholarshipService;

        public AttendanceReportsController(
            IAcademicBranchService academicBranchService,
            IAcademicClassService academicClassService,
            IAcademicGroupService academicGroupService,
            IAcademicSectionService academicSectionService,
            IAcademicSessionService academicSessionService,
            IAcademicPeriodService academicPeriodService,
            IAcademicVersionService academicVersionService,
            IAcademicShiftService academicShiftService,
            IAcademicBranchesOfUserInfoService academicBranchesOfUserInfoService,
            ICoCurricularActivityService coCurricularActivityService,
            IScholarshipService scholarshipService,
            IDepartmentService academicDepartmentService)
        {

            _academicBranchService = academicBranchService;
            _academicClassService = academicClassService;
            _academicGroupService = academicGroupService;
            _academicSectionService = academicSectionService;
            _academicSessionService = academicSessionService;
            _academicPeriodService = academicPeriodService;
            _academicVersionService = academicVersionService;
            _academicShiftService = academicShiftService;
            _academicBranchesOfUserInfoService = academicBranchesOfUserInfoService;
            _coCurricularActivityService = coCurricularActivityService;
            _scholarshipService = scholarshipService;
            _academicDepartmentService = academicDepartmentService;
        }


        [Route("api/newAttendanceReports")]
        [HttpGet]
        public VmCommonSearch newAttendanceReports()
        {
            var objVmCommonSearch = new VmCommonSearch();
            var instituteId = Sessions.InstituteId;
            var userId = Sessions.UserId;

            objVmCommonSearch.AcademicBranchList = _academicBranchesOfUserInfoService.GetKVPUserWise(userId);
            if (objVmCommonSearch.AcademicBranchList.Count==1)
            {
                objVmCommonSearch.AcademicBranchId = objVmCommonSearch.AcademicBranchList.FirstOrDefault().Key;
            }
            objVmCommonSearch.AcademicDepartmentList = _academicDepartmentService.GetKVP(instituteId);
            objVmCommonSearch.AcademicClassList = _academicClassService.GetKVP(instituteId);
            objVmCommonSearch.AcademicSectionList = _academicSectionService.GetKVP(instituteId);
            objVmCommonSearch.AcademicPeriodList = _academicPeriodService.GetKVP(instituteId);
            objVmCommonSearch.AttendanceDate = DateTime.Now;
            return objVmCommonSearch;

        }

        [Route("api/GetAttendanceReports")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAttendanceReports(VmCommonSearch objVmCommonSearch)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["deeppContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                bool isValid = true;

                string strReportName = "AttendanceReports.rpt";

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }


                ReportDocument rd = new ReportDocument();

                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "tpl//AttendanceReports//" + strReportName;
                rd.Load(strRptPath);
                rd.SetParameterValue("@InstituteId", Sessions.InstituteId);
                //rd.SetParameterValue("@PolliShomajID", 0);
                //rd.SetParameterValue("@Year", Year);
                //rd.SetParameterValue("@FromDate", 0);
                //rd.SetParameterValue("@ToDate", 0);
                //rd.SetDatabaseLogon(SConn.UserID, SConn.Password, SConn.DataSource, SConn.InitialCatalog);
                rd.DataSourceConnections[0].IntegratedSecurity = false;
                //strServer, strDatabase, strUserID, strPwd)
                rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                // rd.SetDataSource();
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "AttendanceReports");


                // System.IO.Stream oStream = null;
                // byte[] byteArray = null;
                // oStream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                // byteArray = new byte[oStream.Length];
                // oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                //// FileStream fileStream = null;//new FileStream();
                //    // fileStream.Write(byteArray, 0, byteArray.Length);
                // FileStream fs = new FileStream(strRptPath, FileMode.Create);
                // fs.Write(byteArray, 0, byteArray.Length);
                // response=Request.CreateResponse(HttpStatusCode.Created, "Application.Pdf");
                //response.Content = byteArray;
                //response.C);
                //response.ClearHeaders();
                //response.ContentType = "application/pdf";
                //response.BinaryWrite(byteArray);
                //response.Flush();
                //response.Close();
                rd.Close();
                rd.Dispose();

                //byte[] getBytes = null;
                //System.IO.MemoryStream ms = (MemoryStream)rd.ExportToStream(ExportFormatType.PortableDocFormat);
                //getBytes = ms.ToArray();

                //  System.IO.Stream oStream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


                //var pdf = ExportFormatType.PortableDocFormat;
                //MemoryStream memStream = (MemoryStream)rd.ExportToStream(ExportFormatType.PortableDocFormat);
                //Attachment data = new Attachment(memStream, MediaTypeNames.Application.Pdf);
                //  rd.DataSourceConnections.Clear();
                rd.Refresh();
                rd.Dispose();
                rd.Clone();
                rd.Close();
                //response.Content = new StreamContent(rd.ExportToStream(ExportFormatType.PortableDocFormat));
                //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                //response.Content.Headers.ContentDisposition.FileName = "Transcript.pdf";

                return response;

            }
            catch (Exception ex)
            {
                //return string.Empty;
                throw;
            }
        }

    }
}
