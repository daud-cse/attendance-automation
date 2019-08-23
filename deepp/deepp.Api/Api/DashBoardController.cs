using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api
{
    public class DashBoardController : ApiController
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public DashBoardController(
             IStudentService studentService
            , ITeacherService teacherService
            , IEmployeeService employeeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _employeeService = employeeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/dashboard/summary")]
        public VmDashboard  GetSummary()
        {
            VmDashboard parameterList = new VmDashboard();
            parameterList.TotalStudentsCount = 5000;
            parameterList.TotalTeachersCount = 40;
            parameterList.TotalEmployeesCount = 30;
            parameterList.TotalTeachersRequired = 12;
            parameterList.TotalAvailableSMS = 1560;

            return parameterList;
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
