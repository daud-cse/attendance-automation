using pnsms.Entities.Models;
using pnsms.Entities.StoredProcedures.Models;
using pnsms.Service;
using pnsms.Service.DashBoard;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
namespace pnsms.portal.Controllers
{
    public class TeacherController : Controller
    {
        #region "  -  [  Constractor  ]  -  "


        //[Dependency]
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IVmTeacherService _vmTeacherService;
        private readonly ITeacherService _teacherService;
        private readonly IDashboardService _dashboardService;
        public TeacherController(
             IUnitOfWorkAsync unitOfWork, IVmTeacherService vmTeacherService
            , ITeacherService teacherService
             , IDashboardService dashboardService)
        {
            _unitOfWork = unitOfWork;
            _vmTeacherService = vmTeacherService;
            _teacherService = teacherService;
            _dashboardService = dashboardService;
        }
        #endregion


        public ActionResult GetActiveTeacher()
        {

            var teacher = _teacherService.GetAllTeacher(Sessions.InstituteId, isActive: true).OrderBy(x => x.Designation.Ordering);
            ViewBag.Count = teacher.Count();

            return View("TeacherList", teacher);
        }
        public ActionResult GetHeadMasterList(string searchItem, int? page, int? id)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;
            IList<GlobalHeadmasterList> lstHeadMaster = new List<GlobalHeadmasterList>();
            var objDashboard = _dashboardService.GetDashboard(Sessions.UserId, Sessions.UserInfoTypeId);
            var  plstHeadMaster = (IList<GlobalHeadmasterList>)objDashboard.lstHeadMaster.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                lstHeadMaster = plstHeadMaster.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                lstHeadMaster = plstHeadMaster.Where(p => p.InstituteName.ToLower() == searchItem.ToLower()                                   
                     || Convert.ToString(p.Name).ToLower() == searchItem.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(lstHeadMaster);
        }      
        public ActionResult GetActiveTeacherPartial()
        {

            var teacher = _teacherService.GetAllTeacher(Sessions.InstituteId, isActive: true).OrderBy(x => x.Designation.Ordering);
            ViewBag.Count = teacher.Count();

            return PartialView("_PartialTeacherList", teacher);
        }

        public ActionResult GetAllArchiveTeacher()
        {

            var teacher = _teacherService.GetAllTeacher(Sessions.InstituteId, isActive: true).OrderBy(x => x.Designation.Ordering); ;
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