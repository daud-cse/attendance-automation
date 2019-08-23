using deepp.Entities.Models;
using deepp.Entities.StoredProcedures.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.DashBoard;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace deepp.portal.Controllers
{
    public class AttendanceController : Controller
    {
       


        private readonly IVmUserAttendanceService _vmUserAdendanceService;
        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IUserAttendanceService _userAttendanceService;
        private readonly IUserAttendanceDetailService _userAttendanceDetailService;
        private readonly IStoredProcedures _storedProcedures;
        
        readonly int instituteId = Sessions.InstituteId;
        readonly int userId = Sessions.UserId;
       
        public AttendanceController(
            IVmUserAttendanceService vmUserAdendanceService,
            ITeacherService teacherService,
            IUnitOfWorkAsync unitOfWorkAsync 
            , IUserAttendanceDetailService userAttendanceDetailService
            , IUserAttendanceService userAttendanceService
            , IStoredProcedures storedProcedures)
           
        {
            _vmUserAdendanceService = vmUserAdendanceService;
            _teacherService = teacherService;
            _unitOfWorkAsync = unitOfWorkAsync;            
            _storedProcedures = storedProcedures;
            _userAttendanceService = userAttendanceService;
            _userAttendanceDetailService = userAttendanceDetailService;
           
        }
        public ActionResult newTeacherAttendance()
        {
            VmSearchAttendance vmSearchAttendance = new VmSearchAttendance();
            
            vmSearchAttendance= _vmUserAdendanceService.newTeacherAttendance(instituteId,Sessions.UserId,Sessions.UserInfoTypeId);
            vmSearchAttendance.SearchTeacherAttendanceData = new List<UserAttendance>();
          
            return View("TeacherAttendance", vmSearchAttendance);
        }

        public ActionResult TeacherAttendance(VmSearchAttendance objVmSearchAttendance)
        {            
            VmSearchAttendance vmSearchAttendance = new VmSearchAttendance();
            var objVmSearchAttendanceDataBase=_vmUserAdendanceService.GetTeacherAttendanceList(objVmSearchAttendance);
            vmSearchAttendance = _vmUserAdendanceService.newTeacherAttendance(instituteId, Sessions.UserId, Sessions.UserInfoTypeId);
            objVmSearchAttendanceDataBase.InstituteList = vmSearchAttendance.InstituteList;
            return View(objVmSearchAttendanceDataBase);
        }
        public ActionResult GetTeacherAttendance(int id)
        {
            VmSearchAttendance vmSearchAttendance = new VmSearchAttendance();
            vmSearchAttendance.objUserAttendance = new UserAttendance();
            vmSearchAttendance.lstserAttendanceDetail = new List<UserAttendanceDetail>();
            vmSearchAttendance = _vmUserAdendanceService.newTeacherAttendance(instituteId,Sessions.UserId,Sessions.UserInfoTypeId);
            var userTeacherAttendance = _userAttendanceService.GetUserAttendanceForDetailsById(id, instituteId);
            vmSearchAttendance.InstituteId = userTeacherAttendance.InstituteId == null?0: Convert.ToInt32(userTeacherAttendance.InstituteId);
            vmSearchAttendance.objUserAttendance = userTeacherAttendance;
            vmSearchAttendance.objUserAttendance.UserAttendanceDetails = vmSearchAttendance.objUserAttendance.UserAttendanceDetails.ToList();
            return View(vmSearchAttendance);
        }
    }
}