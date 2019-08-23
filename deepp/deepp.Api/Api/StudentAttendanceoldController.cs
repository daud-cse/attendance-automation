using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.ViewModels;
using deepp.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api
{
    public class StudentAttendanceoldController : ApiController
    {
        private readonly IVmStudentAttendanceService _vmStudentAdendanceService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly ITeacherService _teacherService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        readonly int instituteId = Sessions.InstituteId;
        readonly int userId = Sessions.UserId;
       
        public StudentAttendanceoldController(
            IVmStudentAttendanceService vmStudentAdendanceService,
            IStudentAttendanceService studentAttendanceService,
            ITeacherService teacherService,
            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _vmStudentAdendanceService = vmStudentAdendanceService;
            _studentAttendanceService = studentAttendanceService;
            _teacherService=teacherService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/studentattendanceold/new")]
        public VmStudentAttendance GetNew()
        {
            var studentAttendanceCreate = _vmStudentAdendanceService.GetVmStudentAttendanceByManagement(instituteId,0,1);
            return studentAttendanceCreate;
        }

        [Route("api/studentattendance/listbyteacher")]
        [HttpPost]
        public VmSearchAttendance GetAllByTeacher([FromBody]VmSearchAttendance studentAttendanceModel)
        {
            studentAttendanceModel.UserId = userId;
            var attendanceList = _vmStudentAdendanceService.GeAttendanceList(studentAttendanceModel, instituteId);
            return attendanceList;
        }

        [Route("api/studentattendance/attendancesheetprint")]
        [HttpPost]
        public VmSearchAttendance GetAttendanceSheetPrints([FromBody]VmSearchAttendance studentAttendanceModel)
        {
            var attendanceSheet = _vmStudentAdendanceService.GeAttendanceSheetPrint(studentAttendanceModel, instituteId);
            return attendanceSheet;
        }

        [Route("api/studentattendance/listbymanagement")]
        [HttpPost]
        public VmSearchAttendance GetAllByManagement([FromBody]VmSearchAttendance studentAttendanceModel)
        {
            var attendanceList = _vmStudentAdendanceService.GeAttendanceList(studentAttendanceModel, instituteId);
            return attendanceList;
        }

        
        [Route("api/studentattendance/loadstudents")]
        [HttpPost]
        public VmStudentAttendance GetStudentsList([FromBody]VmStudentAttendance studentAttendanceModel)
        {
            var vmStuAttendanceWithList = _vmStudentAdendanceService.GetVmStudentList(studentAttendanceModel,instituteId);
            return vmStuAttendanceWithList;
        }

        [Route("api/studentattendance/saveattendance")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VmStudentAttendance studentAttendanceModel)
        {
            studentAttendanceModel.StudentAttendance.TeacherId = (studentAttendanceModel.StudentAttendance.TeacherId == 0) ? userId : studentAttendanceModel.StudentAttendance.TeacherId;
            _vmStudentAdendanceService.CreateStudentAttendance(studentAttendanceModel,_unitOfWorkAsync,Sessions.InstituteId);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/studentattendance/")]
        public VmStudentAttendance GetVmStudentAttendanceById(int id)
        {
            var vmStudentAdendance = _vmStudentAdendanceService.GetVmStudentAttendanceEdit(id, instituteId);
            return vmStudentAdendance;
        }

        [Route("api/studentattendance/getsingle/")]
        public VmStudentAttendance GetSingleAttendanceById(int id)
        {
            var vmStudentAdendance = _vmStudentAdendanceService.GetVmStudentAttendanceDetails(id, instituteId);
            return vmStudentAdendance;
        }

        [Route("api/studentattendance/getteachers/")]
        public VmStudentAttendance GetTeachersById(int id)
        {
            var data = _teacherService.GetAllTeacher(instituteId, "", id).ToList();
            var teacherList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => teacherList.Add(new KeyValuePair<int, string>(c.TeacherId, c.UserInfo.Name)));
            VmStudentAttendance studentAttendanceModel = new VmStudentAttendance();
            studentAttendanceModel.StudentAttendance = new StudentAttendance();
            studentAttendanceModel.StudentAttendance.TeacherList = teacherList;
            return studentAttendanceModel;
        }

        [Route("api/studentattendance/updateattendance")]
        [HttpPost]
        public HttpResponseMessage Update([FromBody]VmStudentAttendance studentAttendanceModel)
        {
            _vmStudentAdendanceService.UpdateStudentAttendance(studentAttendanceModel,_unitOfWorkAsync,Sessions.InstituteId);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/studentattendance/updateabsconding")]
        [HttpPost]
        public HttpResponseMessage UpdateAssconding([FromBody]VmStudentAttendance studentAttendanceModel)
        {
            _vmStudentAdendanceService.UpdateAsscondingStudentAttendance(studentAttendanceModel, instituteId, _unitOfWorkAsync);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }


    }
}
