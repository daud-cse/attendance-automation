using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using deepp.erp;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using deepp.Entities.Models;
using deepp.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.erp.Controllers
{
    public class StudentReportsController : Controller
    {
        private readonly IStoredProcedureService _storedProcedureService;

        public StudentReportsController(IStoredProcedureService storedProcedureService)
        {
            _storedProcedureService = storedProcedureService;
        }

        public ActionResult GetStudentsDetails()
        {


            return RedirectToAction(Url.Action("GetStudentsDetailsFinal", new { id = "id" }), new { target = "_blank" });
            //   return RedirectToAction("GetStudentsDetailsFinal");
        }


        public void GetStudentsDetails(string classId, string SectionId)
        {
            ReportDocument crystalReport = new ReportDocument();
            ReportClass rptDoc;
            if (classId == "")
            {
                classId = null;
            }
            if (SectionId == "")
            {
                SectionId = null;
            }
            DataSet dsStudentsDetails = new DataSet();
            dsStudentsDetails = _storedProcedureService.GetStudentReports(Sessions.InstituteId, null, classId, SectionId);
            string strReportName = "StudentsReports.rpt";
            try
            {
                if (dsStudentsDetails.Tables[0].Rows.Count >= 0)
                {
                    rptDoc = new ReportClass();
                    rptDoc.FileName = Server.MapPath("~/") + "Views//StudentReports//" + strReportName;
                    rptDoc.Load();
                    rptDoc.Refresh();
                    rptDoc.SetDataSource(dsStudentsDetails);
                    rptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Projects Details");

                }
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }

        //public void GetMaleFemaleRation(string id)
        //{
        //  // var MaleFemaleRatio= _storedProcedureService.GetMaleFemaleRation(1,1,1,6,1,1,11,1);
        //    var MaleFemaleRatio = _storedProcedureService.test();

        //}
    }
}