using deepp.erp;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Entities.ViewModels.DashBoard;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.erp.Api
{
    public class DashBoardController : ApiController
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IStoredProcedures _storedProcedures;
        int institutionId = Sessions.InstituteId;
        int currentSessionId = Sessions.CurrentSessionId;

        public DashBoardController(
             IStudentService studentService
            , ITeacherService teacherService
            , IEmployeeService employeeService
            , IUnitOfWorkAsync unitOfWorkAsync
            , IStoredProcedures storedProcedures)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _employeeService = employeeService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _storedProcedures = storedProcedures;
        }

        [Route("api/dashboard/summary")]
        public VMInstituteDashBoard GetSummary()
        {         
            var objVMInstituteDashBoard=_storedProcedures.GetInstituteDashBoards(institutionId,currentSessionId);
            return objVMInstituteDashBoard;
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
