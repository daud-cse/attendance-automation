using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.Teacher;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.landing.Controllers
{
    public class TeacherController : Controller
    {

        #region "  -  [  Constractor  ]  -  "


        //[Dependency]
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVmTeacherService _vmTeacherService;
        private readonly ITeacherService _teacherService;

        public TeacherController(
             IUnitOfWork unitOfWork, IVmTeacherService vmTeacherService, ITeacherService teacherService)
        {
            _unitOfWork = unitOfWork;
            _vmTeacherService = vmTeacherService;
            _teacherService = teacherService;
        }
        #endregion

       
        public ActionResult GetActiveTeacher()
        {

            var teacher = _teacherService.GetAllTeacher(Sessions.InstituteId, isActive: true).OrderBy(x => x.Designation.Ordering);
            ViewBag.Count = teacher.Count();

            return View("TeacherList", teacher);
        }
        public ActionResult GetActiveTeacherPartial()
        {

            var teacher = _teacherService.GetAllTeacher(Sessions.InstituteId, isActive: true).OrderBy(x => x.Designation.Ordering);
            ViewBag.Count = teacher.Count();

            return PartialView("_PartialTeacherList", teacher);
        }

        public ActionResult GetAllArchiveTeacher()
        {

            var teacher = _teacherService.GetAllTeacher(Sessions.InstituteId,isActive: true).OrderBy(x => x.Designation.Ordering); ;
            return View("TeacherList", teacher);
        }
        public ActionResult TeacherDetails(int teacherId)
        {

            var objTeacher = _teacherService.GetTeacherById(teacherId);

            if (objTeacher.Department == null)
            {
                objTeacher.Department = new Department();
            }

            if (objTeacher.Designation == null)
            {
                objTeacher.Designation = new Designation();
            }
            return View(objTeacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

    
    }
}
