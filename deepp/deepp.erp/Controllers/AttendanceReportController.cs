using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using deepp.erp;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.erp.Controllers
{
    public class AttendanceReportController : Controller
    {
        private readonly IVmStudentAttendanceService _vmStudentAdendanceService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int instituteId = 1;
        DataSetService objDataSetService = new DataSetService();

        public AttendanceReportController(
            IVmStudentAttendanceService vmStudentAdendanceService,
            IStudentAttendanceService studentAttendanceService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _vmStudentAdendanceService = vmStudentAdendanceService;
            _studentAttendanceService = studentAttendanceService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        public void GetAttendanceReports(int AcademicBranchId = 0, int AcademicClassesId = 0, int AcademicSectionId = 0, int AcademicPeriodId=0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["deeppContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
         
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
                rd.DataSourceConnections[0].IntegratedSecurity = false;                
                rd.DataSourceConnections[0].SetConnection(SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
                // rd.SetDataSource();
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "AttendanceReports");
                rd.Refresh();
                rd.Dispose();
                rd.Clone();
                rd.Close();
                //response.Content = new StreamContent(rd.ExportToStream(ExportFormatType.PortableDocFormat));
                //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                //response.Content.Headers.ContentDisposition.FileName = "Transcript.pdf";

                //return response;

            }
            catch (Exception ex)
            {
                //return string.Empty;
                throw;
            }
        }
        public void GetAttendanceReportsDataSet(int AcademicBranchId = 0, int AcademicClassesId = 0, int AcademicSectionId = 0, int AcademicPeriodId = 0, int Month = 0, int Day = 0, int Year = 0, int StudentId=0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["deeppContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
           
            try
            {
                bool isValid = true;
               
              
               ReportClass rptDoc;
               string strReportName = "AttendanceReports.rpt";

               
                
                string CommandText= "[dbo].[AttendanceReports] " 
                                    + Sessions.InstituteId + "," 
                                    + "'" + Sessions.CurrentSessionId + "'" 
                                    + "," + "'" + AcademicBranchId + "'" + "," + "'"
                                    + AcademicClassesId + "'" + "," + "'" + AcademicSectionId + "'" 
                                    + "," + "'" + AcademicPeriodId + "'" + "," + "'" 
                                    + Year + "'" + "," + "'" + Month + "'" + "," + "'" + Day + "'"
                                    + "," + "'" + StudentId + "'";


                var ds = objDataSetService.GetDataSetObject(CommandText);
              
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptDoc = new ReportClass();
                    rptDoc.FileName = Server.MapPath("~/") + "tpl//AttendanceReports//" + strReportName;
                    rptDoc.Load();
                    rptDoc.Refresh();
                    rptDoc.SetDataSource(ds);
                    rptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "AttendanceReports");
                    rptDoc.DataSourceConnections.Clear();
                    rptDoc.Refresh();
                    rptDoc.Dispose();
                    rptDoc.Clone();
                    rptDoc.Close();
                    GC.Collect();

                }
                //return response;

            }
            catch (Exception ex)
            {
                //return string.Empty;
                throw;
            }
        }
        public void GetUserAttendanceReportsDataSet(int AcademicBranchId = 0, int UserInfoId=0, int Month = 0, int Day = 0, int Year = 0,int AcademicDepartmentId=0, string startDate=null, string endDate =null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["deeppContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;
                ReportClass rptDoc;
                string strReportName = "UserAttendanceReports.rpt";
                if (startDate== "undefined")
                {
                    startDate = null;
                }
                if (endDate == "undefined")
                {
                    endDate = null;
                }

                string CommandText = "[dbo].[SprUserAttendanceReports] "
                                    + Sessions.InstituteId + "," + 13 + ","
                                    + "'" + Sessions.CurrentSessionId + "'"
                                    + "," + "'" + AcademicBranchId + "'" + "," + "'"
                                    + Year + "'" + "," + "'" + Month + "'" + "," + "'" + Day + "'"
                                    + "," + "'" + UserInfoId + "'"+ "," + "'" + AcademicDepartmentId+ "'" + "," + "'" + startDate + "'" + "," + "'" + endDate + "'";


                var ds = objDataSetService.GetDataSetObject(CommandText);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptDoc = new ReportClass();
                    rptDoc.FileName = Server.MapPath("~/") + "tpl//AttendanceReports//" + strReportName;
                    rptDoc.Load();
                    rptDoc.Refresh();
                    rptDoc.SetDataSource(ds);
                    rptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "AttendanceReports");
                    rptDoc.DataSourceConnections.Clear();
                    rptDoc.Refresh();
                    rptDoc.Dispose();
                    rptDoc.Clone();
                    rptDoc.Close();
                    GC.Collect();

                }  
            }
            catch (Exception ex)
            {            
                throw;
            }
        }

        public void GetUserAttendanceSummaryReportsDataSet(int AcademicBranchId = 0, int AcademicSessionId = 0, int UserInfoId = 0, int Month = 0, int Day = 0, int Year = 0, int AcademicDepartmentId = 0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["deeppContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            try
            {
                bool isValid = true;
                ReportClass rptDoc;
                string strReportName = "UserAttendanceSummaryReports.rpt";

                string CommandText = "[dbo].[sp_att_TeacherAttendance_summaryReports] "
                                    + Sessions.InstituteId + "," + 13 + ","
                                    + "'" + AcademicSessionId + "'"
                                    + "," + "'" + AcademicBranchId + "'" + "," + "'"
                                    + Year + "'" + "," + "'" + Month + "'" + "," + "'" + Day + "'"
                                    + "," + "'" + UserInfoId + "'";


                var ds = objDataSetService.GetDataSetObject(CommandText);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptDoc = new ReportClass();
                    rptDoc.FileName = Server.MapPath("~/") + "tpl//AttendanceReports//" + strReportName;
                    rptDoc.Load();
                    rptDoc.Refresh();
                    rptDoc.SetDataSource(ds);
                    rptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Teacher/Employee Monthly Attendance Summary");
                    rptDoc.DataSourceConnections.Clear();
                    rptDoc.Refresh();
                    rptDoc.Dispose();
                    rptDoc.Clone();
                    rptDoc.Close();
                    GC.Collect();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}