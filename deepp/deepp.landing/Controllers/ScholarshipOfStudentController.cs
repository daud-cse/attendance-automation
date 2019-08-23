using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.landing.Controllers
{
    public class ScholarshipOfStudentController : Controller
    {
          #region "  -  [  Constractor  ]  -  "

        private readonly IUnitOfWork _unitOfWork;
        private readonly IScholarshipOfStudentService _iScholarshipOfStudentService;

        public ScholarshipOfStudentController(
             IUnitOfWork unitOfWork,          
            IScholarshipOfStudentService iScholarshipOfStudentService)
        {
            _unitOfWork = unitOfWork;
            _iScholarshipOfStudentService = iScholarshipOfStudentService;
        }
        #endregion


        public ActionResult GetScholarshipOfStudent()
        {
            var scholarshipOfStudent = _iScholarshipOfStudentService.GetScholarshipOfStudentByInstituteId(Sessions.InstituteId);
            return View("ScholarStudent", scholarshipOfStudent);
        }
        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disdeepping);
        }

	}
}