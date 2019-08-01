using pnsms.Entities.Models;
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
    public class EmployeesController : Controller
    {
        #region "  -  [  Constractor  ]  -  "


        //[Dependency]
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IVmEmployeeService _vmEmployeeService;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(
             IUnitOfWork unitOfWork,
            //IVmEmployeeService vmEmployeeService,
            IEmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
           // _vmEmployeeService = vmEmployeeService;
            _employeeService = employeeService;
        }
        #endregion


        public ActionResult GetActiveEmployees()
        {

            var employee = _employeeService.GetAllEmployee(Sessions.InstituteId)
                .OrderBy(x => (x.Designation == null ? 999999999 : x.Designation.Ordering)).ToList();

          
            ViewBag.Count = employee.Count();
            
            return View("EmployeesList", employee);
        }
        public ActionResult EmployeesDetails(int employeeId)
        {

            var objEmployees = _employeeService.GetEmployeeById(employeeId);

            if (objEmployees.Department == null)
            {
                objEmployees.Department = new Department();
            }

            if (objEmployees.Designation == null)
            {
                objEmployees.Designation = new Designation();
            }
            return View(objEmployees);
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