
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service.Exams
{

    public interface IVmExamTabulationSheetService
    {
        VmExamTabulationSheet GetVmExamTabulationSheet(int instituteId, int examtypeid, int studentid, int sessionid);
        VmExamTabulationSheet GetVmExamTabulationSheetForPortal(int instituteId, int examTypeId, int userId, int sessionId);
    }
    public class VmExamTabulationSheetService : IVmExamTabulationSheetService
    {

        private readonly IStoredProcedureService _storedProcedureService;
        private readonly IExamTypeWiseTabulationSheetMasterService _examTypeWiseTabulationSheetMasterService;
        private readonly IExamTypeWiseTabulationSheetDetailService _examTypeWiseTabulationSheetDetailService;

        public VmExamTabulationSheetService(
             IStoredProcedureService storedProcedureService
            ,IExamTypeWiseTabulationSheetMasterService examTypeWiseTabulationSheetMasterService
            , IExamTypeWiseTabulationSheetDetailService examTypeWiseTabulationSheetDetailService

            )
        {

            _storedProcedureService = storedProcedureService;
            _examTypeWiseTabulationSheetMasterService = examTypeWiseTabulationSheetMasterService;
            _examTypeWiseTabulationSheetDetailService = examTypeWiseTabulationSheetDetailService;

        }

        //public VmExamTabulationSheet GetVmExamTabulationSheet(int instituteid, int examtypeId, int studentId)
        //{
        //    var objVmExamTabulationSheet = _storedProcedureService.GetVmExamTabulationSheet(instituteid, examtypeId, studentId);

        //    return objVmExamTabulationSheet;
        //}
        public VmExamTabulationSheet GetVmExamTabulationSheet(int instituteId, int examtypeid, int studentid, int sessionid)
        {
            VmExamTabulationSheet objVmExamTabulationSheet = new VmExamTabulationSheet();
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster = new ExamTypeWiseTabulationSheetMaster();
            objVmExamTabulationSheet.lstExamTypeWiseTabulationSheetDetails = new List<ExamTypeWiseTabulationSheetDetail>();

            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster = _examTypeWiseTabulationSheetMasterService.GetExamTabulationSheetMaster(instituteId, examtypeid, studentid, sessionid);
            objVmExamTabulationSheet.lstExamTypeWiseTabulationSheetDetails = _examTypeWiseTabulationSheetDetailService.GetExamTabulationSheetDetails(instituteId, examtypeid,studentid, sessionid).ToList();
            return objVmExamTabulationSheet;
        }
        public VmExamTabulationSheet GetVmExamTabulationSheetForPortal(int instituteId, int examtypeid, int studentid, int sessionid)
        {
            VmExamTabulationSheet objVmExamTabulationSheet = new VmExamTabulationSheet();
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster = new ExamTypeWiseTabulationSheetMaster();
            objVmExamTabulationSheet.lstExamTypeWiseTabulationSheetDetails = new List<ExamTypeWiseTabulationSheetDetail>();

            var objobjExamTypeWiseTabulationSheetMaster = _examTypeWiseTabulationSheetMasterService.GetExamTabulationSheetMaster(instituteId, examtypeid, studentid, sessionid);
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.ExamType = objobjExamTypeWiseTabulationSheetMaster.ExamType;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.AcademicClassName = objobjExamTypeWiseTabulationSheetMaster.AcademicClassName;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.StudentName = objobjExamTypeWiseTabulationSheetMaster.StudentName;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.AcademicSectionName = objobjExamTypeWiseTabulationSheetMaster.AcademicSectionName;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.TotalMarks = objobjExamTypeWiseTabulationSheetMaster.TotalMarks;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.TotalSubject = objobjExamTypeWiseTabulationSheetMaster.TotalSubject;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.AverageNumber = objobjExamTypeWiseTabulationSheetMaster.AverageNumber;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.ExamGradePoint = objobjExamTypeWiseTabulationSheetMaster.ExamGradePoint;
            objVmExamTabulationSheet.objExamTypeWiseTabulationSheetMaster.ExamGradeName = objobjExamTypeWiseTabulationSheetMaster.ExamGradeName;


            objVmExamTabulationSheet.lstExamTypeWiseTabulationSheetDetails = _examTypeWiseTabulationSheetDetailService.GetExamTabulationSheetDetails(instituteId, examtypeid, studentid, sessionid).ToList()
                .Select(x => new ExamTypeWiseTabulationSheetDetail
                {
                    ExamGradeName = x.ExamGradeName,
                    InstituteSubjectName = x.InstituteSubjectName,
                    TotalMarks = x.TotalMarks,
                    SubjectMarks = x.SubjectMarks,
                    AcceptTotalMarks = x.AcceptTotalMarks,
                    AverageMarks = x.AverageMarks,
                    ExamGradePoint = x.ExamGradePoint
                }).ToList();
            return objVmExamTabulationSheet;
                }
    }
}

