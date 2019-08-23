using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Entities.ViewModels.Exams;
using deepp.Service;
using deepp.Service.Exams;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace deepp.portal.Controllers
{
    public class StudentResultController : Controller
    {

        //   private readonly IExamTypeService _examTypeServic;
        private readonly IRepositoryAsync<ExamSubjectMark> _redeeppitory;
        private readonly IVmExamTabulationSheetService _vmExamTabulationSheetService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAcademicSessionService _academicSessionService;
        private readonly IExamSubjectMarkService _examSubjectMarkService;
        private readonly IStudentService _studentService;
        private readonly IExamService _ExamService;


        public StudentResultController(IAcademicSessionService academicSessionService
            , IUnitOfWork unitOfWork
            , IVmExamTabulationSheetService vmExamTabulationSheetService
            , IExamSubjectMarkService examSubjectMarkService
            , IStudentService studentService
            , IExamService ExamService, IRepositoryAsync<ExamSubjectMark> redeeppitory)
        {

            //_examTypeServic = examTypeServic;
            _redeeppitory = redeeppitory;
            _vmExamTabulationSheetService = vmExamTabulationSheetService;
            _unitOfWork = unitOfWork;
            _academicSessionService = academicSessionService;
            _examSubjectMarkService = examSubjectMarkService;
            _studentService = studentService;
            _ExamService = ExamService;
        }
        // GET: StudentResult
        public ActionResult Index()
        {
            var instituteId = Sessions.InstituteId;

            var userId = Sessions.UserId;
            var newExamResultShow = _examSubjectMarkService.newExamResultShow(instituteId);
            newExamResultShow.ExamList = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>>();
            return View(newExamResultShow);
        }

        public JsonResult GetExamBySearchCriteria(int sessionId, int ExamTypeId)
        {
            VmCommonSearch objVmCommonSearch = new VmCommonSearch();

            var instituteId = Sessions.InstituteId;

            var userId = Sessions.UserId;
            objVmCommonSearch.InstituteId = instituteId;
          
            //            objVmCommonSearch.

            var objStudent=_studentService.Find(userId);

            objVmCommonSearch.AcademicClassesId = objStudent.CurrentAcademicClassId == null ? 0 : Convert.ToInt32(objStudent.CurrentAcademicClassId);
            objVmCommonSearch.AcademicClassesSectionMapId=objStudent.CurrentAcademicSectionId==null?0: Convert.ToInt32(objStudent.CurrentAcademicSectionId);
            objVmCommonSearch.ExamTypeId = ExamTypeId;
            var examType = _ExamService.GetKVP(objVmCommonSearch);
            
            return Json(examType, JsonRequestBehavior.AllowGet);

            //      public JsonResult GetDistrictByDivisionId(int divisionId)
            //{
            //    var result = _iDistrictService.GetDistrictByDivisionID(divisionId);
            //    return Json(result.kvpDistrict, JsonRequestBehavior.AllowGet);
            //}



        }
        //Get Exam Tabulation Sheet Data
        public JsonResult StudentResultSheet(int sessionId, int ExamTypeId) {
            var instituteId = Sessions.InstituteId;
            var userId = Sessions.UserId;

            


                var resultSheet = _vmExamTabulationSheetService.GetVmExamTabulationSheetForPortal(instituteId, ExamTypeId, userId, sessionId);
                 return Json(resultSheet, JsonRequestBehavior.AllowGet);

        }

        public JsonResult StudentResultSheetByExamName(int sessionId, int ExamTypeId, int ExamId )
        {
            var instituteId = Sessions.InstituteId;
            var userId = Sessions.UserId;
            ExamSubjectMark objVmCommonSearch = new ExamSubjectMark();
            objVmCommonSearch.StudentId = userId;
            objVmCommonSearch.InstituteId = instituteId;
          
            objVmCommonSearch.ExamTypeId = ExamTypeId;
            objVmCommonSearch.ExamId = ExamId;

            
                var lstSearchExamSubjectMarkList = _examSubjectMarkService.Query(x => x.InstituteId == objVmCommonSearch.InstituteId && x.StudentId == objVmCommonSearch.StudentId && x.ExamId == objVmCommonSearch.ExamId)

                                               .Include(x => x.Student.UserInfo).Select().ToList();



                return Json(lstSearchExamSubjectMarkList, JsonRequestBehavior.AllowGet);
       
            

        }





    }
}