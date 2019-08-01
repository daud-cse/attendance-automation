//using pnsms.Entities.ViewModels;
//using pnsms.Service;
//using pnsms.Service.ViewModels;
//using Repository.Pattern.UnitOfWork;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace sfa.Api.Api.Teachers
//{
//    public class TeacherAttendanceoldController : ApiController
//    {
        
//        private readonly IVmUserAttendanceService _vmUserAdendanceService;
//        private readonly ITeacherService _teacherService;
//        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
//        readonly int instituteId = Sessions.InstituteId;
//        readonly int userId = Sessions.UserId;

//        public TeacherAttendanceoldController(
//            IVmUserAttendanceService vmUserAdendanceService,
//            ITeacherService teacherService,
//            IUnitOfWorkAsync unitOfWorkAsync)
//        {
//            _vmUserAdendanceService = vmUserAdendanceService;
//            _teacherService=teacherService;
//            _unitOfWorkAsync = unitOfWorkAsync;
//        }


//        [Route("api/teacherattendance/new")]
//        [HttpPost]
//        public VmUserAttendance GetNew([FromBody]VmUserAttendance userAttendanceModel)
//        {
//            int branchId=(userAttendanceModel != null) ? userAttendanceModel.UserAttendance.AcademicBranchId : 0;
//            var userAttendanceCreate = _vmUserAdendanceService.CreateVmTeacherAttendance(instituteId, branchId);
//            return userAttendanceCreate;
//        }

//        [Route("api/teacherattendance/saveteacherattendance")]
//        [HttpPost]
//        public HttpResponseMessage Post([FromBody]VmUserAttendance userAttendanceModel)
//        {
//            _vmUserAdendanceService.SaveTeacherAttendance(userAttendanceModel, _unitOfWorkAsync);
//            return new HttpResponseMessage(HttpStatusCode.Created);
//        }

//        [Route("api/teacherattendance/updateteacherattendance")]
//        [HttpPost]
//        public HttpResponseMessage Update([FromBody]VmUserAttendance userAttendanceModel)
//        {
//            _vmUserAdendanceService.UpdateUserAttendance(userAttendanceModel, _unitOfWorkAsync);
//            return new HttpResponseMessage(HttpStatusCode.Created);
//        }

//        [Route("api/teacherattendance/list")]
//        [HttpPost]
//        public VmSearchAttendance GetAllTeacherAttendance([FromBody]VmSearchAttendance searchAttendanceModel)
//        {
//            var attendanceList = _vmUserAdendanceService.GetTeacherAttendanceList(searchAttendanceModel, instituteId);
//            return attendanceList;
//        }

//        [Route("api/teacherattendance/getsingle/")]
//        public VmUserAttendance GetSingleAttendanceById(int id)
//        {
//            var vmStudentAdendance = _vmUserAdendanceService.GetVmTeacherDetailsById(id, instituteId);
//            return vmStudentAdendance;
//        }


//    }
//}
