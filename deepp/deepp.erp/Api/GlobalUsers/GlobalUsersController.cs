using deepp.erp;
using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels.GlobalUsers;
using pnsms.erp.Attributes;
using pnsms.Service.GlobalUsers;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api.GlobalUsers
{
    public class GlobalUsersController : ApiController
    {
        #region "  -  [  Constractor  ]  -  "


        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        [Dependency]
        public IGlobalUserService EmployeeService { get; set; }
        [Dependency]
        public IVmGlobalUsersService VmGlobalUsersService { get; set; }


        #endregion

        #region "  -  [  CRUD  ]  -  "

        // GET api/GlobalUsers

        public IEnumerable<VmGlobalUsersDetails> Get(string searchText = "", int? branchId = null)
        {
            return VmGlobalUsersService.GetAllEmployeeDetails(Sessions.InstituteId, searchText, branchId);
        }

        public VmGlobalUsers Get(int id)
        {
            var student = VmGlobalUsersService.GetVmGlobalUsersById(Sessions.InstituteId, Sessions.UserId, id);
            return student;
        }

        public HttpResponseMessage Post([FromBody]VmGlobalUsers vmEmployee)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmEmployee.ProfileImage = images[0];
                vmEmployee.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            VmGlobalUsersService.CreateEmployee(UnitOfWorkAsync, Sessions.InstituteId, vmEmployee);

            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = vmEmployee.UserInfo.Id,
                    Message = "Success"
                })
            };
        }

        // PUT api/GlobalUsers/5

        public void Put(int id, [FromBody]VmGlobalUsers vmEmployee)
        {
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                vmEmployee.ProfileImage = images[0];
                vmEmployee.ProfileImageSmall = images[1];
                Sessions.Temp = null;
            }
            VmGlobalUsersService.UpdateEmployee(UnitOfWorkAsync, vmEmployee);

        }

        // DELETE api/Employee/5

        public void Delete(int id)
        {
        }
        [HttpPost]
        [Route("api/GlobalUsers/search")]
        public IEnumerable<VmGlobalUsersDetails> SearchEmployee(string searchText = "", int? branchId = null)
        {
            return VmGlobalUsersService.GetAllEmployeeDetails(Sessions.InstituteId, searchText, branchId);
        }
        #endregion

        #region "  -  [  Others  ]  -  "
        // GlobalUsers 
        [Route("api/GlobalUsers/new")]
        public VmGlobalUsers GetNewEmployee()
        {
            var student = VmGlobalUsersService.GetNewVmGlobalUsers(Sessions.InstituteId, Sessions.UserId);
            return student;
        }

        // new GlobalUsers only
        [Route("api/GlobalUsers/newGlobalUsers")]
        public GlobalUser GetNewEmployeeModel()
        {
            var student = EmployeeService.NewGlobalUser(Sessions.InstituteId, Sessions.UserId);
            return student;
        }

        /// <summary>
        /// Gets all SMS details.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns></returns>
      


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
