using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class ExamVMController : Controller
    {
        #region "  -  [  Constractor  ]  -  "


        //[Dependency]
        private readonly IUnitOfWork _unitOfWork;

        private readonly IExamGradeService _examGradesevice;
        public ExamVMController(
             IUnitOfWork unitOfWork,IExamGradeService  examGradesevice)
        {
            _unitOfWork = unitOfWork;
           
            _examGradesevice = examGradesevice;
        }
        public ActionResult GetExamGradingPolicy()
        {

            var examGrade = _examGradesevice.GetExamGradeByInstituteId(Sessions.InstituteId);
            return View(examGrade);
        }
        public ActionResult MeritList()
        {

            //var MeritList = _examGradesevice.(Sessions.InstituteId);
            return View();
        }

        public ActionResult Faillist()
        {

            //var MeritList = _examGradesevice.(Sessions.InstituteId);
            return View();
        }

        public ActionResult FindResult()
        {

            //var MeritList = _examGradesevice.(Sessions.InstituteId);
            return View();
        }


        #endregion

    }
}