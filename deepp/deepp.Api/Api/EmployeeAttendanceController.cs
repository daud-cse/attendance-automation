using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api
{
    public class EmployeeAttendanceController : ApiController
    {
        private readonly IVmUserAttendanceService _vmUserAdendanceService;
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        readonly int instituteId = Sessions.InstituteId;
        readonly int userId = Sessions.UserId;

        public EmployeeAttendanceController(
            IVmUserAttendanceService vmUserAdendanceService,
            IEmployeeService employeeService,
            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _vmUserAdendanceService = vmUserAdendanceService;
            _employeeService = employeeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/employeeattendance/new")]
        [HttpPost]
        public VmUserAttendance GetNew([FromBody]VmUserAttendance userAttendanceModel)
        {
            int branchId=(userAttendanceModel != null) ? userAttendanceModel.UserAttendance.AcademicBranchId : 0;
            var userAttendanceCreate = _vmUserAdendanceService.CreateVmEmployeeAttendance(instituteId, branchId);
            return userAttendanceCreate;
        }

        [Route("api/employeeattendance/saveemployeeattendance")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VmUserAttendance userAttendanceModel)
        {
            _vmUserAdendanceService.SaveEmployeeAttendance(userAttendanceModel, _unitOfWorkAsync);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/employeeattendance/updateemployeeattendance")]
        [HttpPost]
        public HttpResponseMessage Update([FromBody]VmUserAttendance userAttendanceModel)
        {
            _vmUserAdendanceService.UpdateUserAttendance(userAttendanceModel, _unitOfWorkAsync);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/employeeattendance/list")]
        [HttpPost]
        public VmSearchAttendance GetAllemployeeattendance([FromBody]VmSearchAttendance searchAttendanceModel)
        {
            var attendanceList = _vmUserAdendanceService.GetEmployeeAttendanceList(searchAttendanceModel, instituteId);
            return attendanceList;
        }

        [Route("api/employeeattendance/getsingle/")]
        public VmUserAttendance GetSingleAttendanceById(int id)
        {
            var vmStudentAdendance = _vmUserAdendanceService.GetVmEmployeeDetailsById(id, instituteId);
            return vmStudentAdendance;
        }










    }
}
