using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Entities.ViewModels.Employee;
using pnsms.Entities.ViewModels.Student;
using pnsms.Service;
using pnsms.Service.ViewModels;
using pnsms.erp.Attributes;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "


        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        [Dependency]
        public IEmployeeService EmployeeService { get; set; }
        [Dependency]
        public IVmEmployeeService VmEmployeeService { get; set; }
         
 
        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/Employee

        public IEnumerable<VmEmployeeDetails> Get(string searchText = "", int? branchId = null)
        {
            return VmEmployeeService.GetAllEmployeeDetails(Sessions.InstituteId, searchText, branchId);
        }

        public VmEmployee Get(int id)
        {
            var student = VmEmployeeService.GetVmEmployeeById(Sessions.InstituteId,Sessions.UserId, id);
            return student;
        }

        public HttpResponseMessage Post([FromBody]VmEmployee vmEmployee)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmEmployee.ProfileImage = images[0];
                vmEmployee.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            VmEmployeeService.CreateEmployee(UnitOfWorkAsync, Sessions.InstituteId, vmEmployee);

            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = vmEmployee.UserInfo.Id,
                    Message = "Success"
                })
            };
        }

        // PUT api/Employee/5

        public void Put(int id, [FromBody]VmEmployee vmEmployee)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmEmployee.ProfileImage = images[0];
                vmEmployee.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            VmEmployeeService.UpdateEmployee(UnitOfWorkAsync, vmEmployee);

        }

        // DELETE api/Employee/5

        public void Delete(int id)
        {
        }
        [HttpPost]
        [Route("api/employee/search")]
        public IEnumerable<VmEmployeeDetails> SearchEmployee(string searchText = "", int? branchId = null)
        {
            return VmEmployeeService.GetAllEmployeeDetails(Sessions.InstituteId, searchText, branchId);
        }
        #endregion

        #region "  -  [  Others  ]  -  "
        // VmEmployee 
        [Route("api/employee/new")]
        public VmEmployee GetNewEmployee()
        {
            var student = VmEmployeeService.GetNewVmEmployee(Sessions.InstituteId, Sessions.UserId);
            return student;
        }

        // new Employee only
        [Route("api/employee/newEmployee")]
        public Employee GetNewEmployeeModel()
        {
            var student = EmployeeService.NewEmployee(Sessions.InstituteId, Sessions.UserId);
            return student;
        }

        /// <summary>
        /// Gets all SMS details.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/employee/getAllSmsDetails")]
        public IEnumerable<ShortMessageDetail> GetAllSmsDetails(Employee employee, int? branchId = null)
        {
            return EmployeeService.GetAllShortMessageDetail(Sessions.InstituteId, Sessions.UserId, employee,branchId);

        }


        #endregion

        #region "  -  [  Others  ]  -  "
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
         
    }


}
