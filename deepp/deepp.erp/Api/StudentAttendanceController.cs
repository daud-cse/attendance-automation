using deepp.erp;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Entities.ViewModels.Attendance;
using pnsms.Service;
using pnsms.Service.ViewModels;
using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    public class StudentAttendanceController : ApiController
    {
        private readonly IVmStudentAttendanceService _vmStudentAdendanceService;
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        readonly int instituteId = Sessions.InstituteId;
        readonly int userId = Sessions.UserId;
       
        public StudentAttendanceController(
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


        [Route("api/studentattendance/newteacher")]
        public List<TakeAttendance> Getnewteacher()
        {
            //Sessions.UserId
            var lstTakeAttendance = _vmStudentAdendanceService.GetVmStudentAttendanceByTeacher(instituteId, Sessions.CurrentSessionId, Sessions.UserId);
            return lstTakeAttendance;
        }
        [Route("api/studentattendance/newmanagement")]
        public VmStudentAttendance Getnewmanagement()
        {
            var studentAttendanceCreate = _vmStudentAdendanceService.GetVmStudentAttendanceByManagement(instituteId, Sessions.CurrentSessionId, userId);
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
            var objvmStuAttendance= _vmStudentAdendanceService.GetVmStudentList(studentAttendanceModel,instituteId);
            return objvmStuAttendance;
        }
        [Route("api/studentattendance/loadstudentsforteacher")]
        [HttpPost]
        public VmStudentAttendance GetStudentsList([FromBody]TakeAttendance objTakeAttendance)
        {

            
            VmStudentAttendance objVmStudentAttendance = new VmStudentAttendance();

            objVmStudentAttendance.StudentAttendance = new StudentAttendance();
            StudentAttendance objStudentAttendance = new StudentAttendance();
            objStudentAttendance.AttendanceDate = objTakeAttendance.Attendancedate;
            objStudentAttendance.InstituteId = Sessions.InstituteId;
            objStudentAttendance.TeacherId = objTakeAttendance.TeacherId;
            objStudentAttendance.TeacherName = objTakeAttendance.TeacherName;
            objStudentAttendance.AcademicSessionId = Sessions.CurrentSessionId;
            objStudentAttendance.AcademicClassId = objTakeAttendance.AcademicClassId;

            objStudentAttendance.BranchName = objTakeAttendance.AcademicBranchName;
            objStudentAttendance.AcademicBranchId = objTakeAttendance.AcademicBranchId;


            objStudentAttendance.ClassName = objTakeAttendance.AcademicClassName;
            objStudentAttendance.AcademicSessionId = objTakeAttendance.AcademicSessionId;
            objStudentAttendance.AcademicSectionId = objTakeAttendance.AcademicClassSectionMapId;
            objStudentAttendance.SectionName = objTakeAttendance.AcademicSectionName;
          
            objStudentAttendance.SubjectAcademicClassMappingsId = objTakeAttendance.SubjectAcademicClassMappingsId;
            objStudentAttendance.SubjectName = objTakeAttendance.SubjectName;

         


            objVmStudentAttendance.StudentAttendance = objStudentAttendance;
            var objvmStuAttendance = _vmStudentAdendanceService.GetVmStudentList(objVmStudentAttendance, instituteId);
            return objvmStuAttendance;
        }
        [Route("api/studentattendance/saveattendance")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VmStudentAttendance studentAttendanceModel)
        {
            try
            {
                studentAttendanceModel.StudentAttendance.AcademicSessionId = Sessions.CurrentSessionId;
                studentAttendanceModel.StudentAttendance.TeacherId = (studentAttendanceModel.StudentAttendance.TeacherId == 0) ? userId : studentAttendanceModel.StudentAttendance.TeacherId;
                _vmStudentAdendanceService.CreateStudentAttendance(studentAttendanceModel, _unitOfWorkAsync, Sessions.InstituteId);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch(Exception ex){
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }
          
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
            studentAttendanceModel.StudentAttendance.AcademicSessionId = Sessions.CurrentSessionId;
            _vmStudentAdendanceService.UpdateStudentAttendance(studentAttendanceModel,_unitOfWorkAsync,Sessions.InstituteId);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/studentattendance/updateabsconding")]
        [HttpPost]
        public HttpResponseMessage UpdateAssconding([FromBody]VmStudentAttendance studentAttendanceModel)
        {
            studentAttendanceModel.StudentAttendance.AcademicSessionId = Sessions.CurrentSessionId;
            _vmStudentAdendanceService.UpdateAsscondingStudentAttendance(studentAttendanceModel, instituteId, _unitOfWorkAsync);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
