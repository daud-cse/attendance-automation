using deepp.Entities.Models;
using deepp.Entities.ResponseModel;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.SSOLogin;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api.Attendance
{
    public class StudentAttendanceController : ApiController
    {
        private readonly IVmStudentAttendanceService _vmStudentAdendanceService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly IStudentAttendanceDetailService _studentAttendanceDetailService;
        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISSOService _SSOService;
        readonly int instituteId = 5;// Sessions.InstituteId;
        readonly int userId = 5;// Sessions.UserId;

        public StudentAttendanceController(
            IVmStudentAttendanceService vmStudentAdendanceService,
            IStudentAttendanceService studentAttendanceService,
            IStudentAttendanceDetailService studentAttendanceDetailService,
            ITeacherService teacherService,
            ISSOService SSOService,
            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _vmStudentAdendanceService = vmStudentAdendanceService;
            _studentAttendanceService = studentAttendanceService;
            _studentAttendanceDetailService = studentAttendanceDetailService;
            _teacherService = teacherService;
            _SSOService = SSOService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/studentattendance/new")]
        public VmStudentAttendance GetNew()
        {
            var instituteid = 5;
            var studentAttendanceCreate = _vmStudentAdendanceService.GetVmStudentAttendanceByManagement(instituteid, 0, 1);
            return studentAttendanceCreate;
        }
        [Route("api/getstudentattendance")]
        public List<StudentAttendance> getstudentattendance()
        {

            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                List<StudentAttendance> StudentAttendancelst = new List<StudentAttendance> { new StudentAttendance { ClassName = "Token Is not found" } };
                return StudentAttendancelst;
            }
            else { return _studentAttendanceService.GeStudentAttendanceList(objSSO.InstituteId); }


        }
        [Route("api/getstudentattendancedetails")]
        public List<StudentAttendanceDetail> getstudentattendancedetails()
        {

            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                List<StudentAttendanceDetail> StudentAttendanceDetaillst = new List<StudentAttendanceDetail> { new StudentAttendanceDetail { Comments = "Token Is not found" } };
                return StudentAttendanceDetaillst;
            }
            else { return _studentAttendanceDetailService.GeStudentAttendanceDetailsList(objSSO.InstituteId); }
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
            var vmStuAttendanceWithList = _vmStudentAdendanceService.GetVmStudentList(studentAttendanceModel, instituteId);
            return vmStuAttendanceWithList;
        }

        [Route("api/studentattendance/synsaveattendance")]
        [HttpPost]
        public SynStudentAttendanceReponseModel Post([FromBody]List<StudentAttendance> lststudentAttendance)
        {
            SynStudentAttendanceReponseModel objSynStudentAttendanceReponseModel = new SynStudentAttendanceReponseModel();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                objSynStudentAttendanceReponseModel.Token = "Token Not Found";
                return objSynStudentAttendanceReponseModel;
            }
            else
            {

                objSynStudentAttendanceReponseModel = _vmStudentAdendanceService.CreateUpdateStudentAttendance(lststudentAttendance, _unitOfWorkAsync, objSSO.InstituteId, objSSO.AcademicSessionId, objSSO.UserId);
                // response = Request.CreateResponse(HttpStatusCode.OK, objSynStudentAttendanceReponseModel);
                //objSynStudentAttendanceReponseModel.Obj = objSynStudentAttendanceReponseModel;

                objSynStudentAttendanceReponseModel.Obj = null;
             
                return objSynStudentAttendanceReponseModel;

            }
        }

        [Route("api/studentattendance/")]
        public VmStudentAttendance GetVmStudentAttendanceById(int id)
        {

            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                VmStudentAttendance objVmStudentAttendance = new VmStudentAttendance();
                objVmStudentAttendance.msg = "Token Is not found";
                // { new VmStudentAttendance { msg = "Token Is not found" } };
                return objVmStudentAttendance;
            }
            else
            {
                var vmStudentAdendance = _vmStudentAdendanceService.GetVmStudentAttendanceEdit(id, objSSO.InstituteId);
                return vmStudentAdendance;
            }

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
            _vmStudentAdendanceService.UpdateStudentAttendance(studentAttendanceModel, _unitOfWorkAsync, Sessions.InstituteId);
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
