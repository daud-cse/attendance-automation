using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.erp.Controllers
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
            string connectionString = ConfigurationManager.ConnectionStrings["PNSMSContext"].ConnectionString;
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
            string connectionString = ConfigurationManager.ConnectionStrings["PNSMSContext"].ConnectionString;
            SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);

            //int year = 0;
            try
            {
                bool isValid = true;
               
               // //SqlCommand cmd = new SqlCommand();
               // SqlParameterCollection pc = objDataSetService.SetSqlParameter();
              
               ReportClass rptDoc;
               string strReportName = "AttendanceReports.rpt";

               // pc.Add("@InstituteId", SqlDbType.Int);
               // pc["@InstituteId"].Value = Sessions.InstituteId;

               // pc.Add("@AcademicSessionId", SqlDbType.Int);
               // pc["@AcademicSessionId"].Value = 0;// Sessions.CurrentSessionId;


               // pc.Add("@AcademicBranchId", SqlDbType.Int);
               // pc["@AcademicBranchId"].Value = 0;// AcademicBranchId;

               // pc.Add("@AcademicClassesId", SqlDbType.Int);
               // pc["@AcademicClassesId"].Value = 0;// AcademicClassesId;

               // pc.Add("@AcademicSectionId", SqlDbType.Int);
               // pc["@AcademicSectionId"].Value = 0;// AcademicSectionId;

               // pc.Add("@AcademicPeriodId", SqlDbType.Int);
               // pc["@AcademicPeriodId"].Value = 0;// AcademicPeriodId;

               // pc.Add("@Year", SqlDbType.Int);
               // pc["@Year"].Value = 0;

               // pc.Add("@Month", SqlDbType.Int);
               // pc["@Month"].Value = 0;

               // pc.Add("@Day", SqlDbType.Int);
               // pc["@Day"].Value = 0;

               // pc.Add("@StudentId", SqlDbType.Int);
               // pc["@StudentId"].Value = 0;

                //PNSMSContext _contex = new PNSMSContext();
                //SqlConnection con = new SqlConnection();
                //SqlCommand cmd = new SqlCommand();
                //con.ConnectionString = _contex.Database.Connection.ConnectionString;
                //cmd.Connection = con;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "[dbo].[AttendanceReports] " + Sessions.InstituteId + "," + "'" + Sessions.CurrentSessionId + "'" + "," + "'" + AcademicBranchId + "'" + "," + "'" + AcademicClassesId + "'" + "," + "'" + AcademicSectionId + "'" + "," + "'" + AcademicPeriodId + "'" + "," + "'" + Year + "'" + "," + "'" + Month + "'" + "," + "'" + Day + "'" + "," + "'" + StudentId + "'";
                //DataSet ds = new DataSet();
                //System.Data.SqlClient.SqlDataAdapter ad = new SqlDataAdapter();
                //ad.SelectCommand = cmd;
                //ad.Fill(ds);
                //con.Close();
                //con.Dispose();
                
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
            string connectionString = ConfigurationManager.ConnectionStrings["PNSMSContext"].ConnectionString;
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

    }
}