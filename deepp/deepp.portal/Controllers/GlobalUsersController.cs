using Microsoft.Practices.Unity;
using deepp.Entities.Models;
using deepp.Entities.ViewModels.GlobalUsers;
using deepp.Service.GlobalUsers;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace deepp.portal.Controllers
{
    public class GlobalUsersController : Controller
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

        // GET api/Employee

        public IEnumerable<VmGlobalUsersDetails> Get(string searchText = "", int? branchId = null)
        {
            return VmGlobalUsersService.GetAllEmployeeDetails(Sessions.InstituteId, searchText, branchId);
        }

        public VmGlobalUsers GetGlobaluser(int id)
        {
            var student = VmGlobalUsersService.GetVmGlobalUsersById(Sessions.InstituteId, Sessions.UserId, id);
            return student;
        }

     

        // PUT api/Employee/5

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
       
        public IEnumerable<VmGlobalUsersDetails> SearchEmployee(string searchText = "", int? branchId = null)
        {
            return VmGlobalUsersService.GetAllEmployeeDetails(Sessions.InstituteId, searchText, branchId);
        }
        #endregion

        #region "  -  [  Others  ]  -  "
        // VmEmployee 
       
        public VmGlobalUsers GetNewEmployee()
        {
            var student = VmGlobalUsersService.GetNewVmGlobalUsers(Sessions.InstituteId, Sessions.UserId);
            return student;
        }

        // new Employee only
     
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
        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }
        #endregion
    
}
}