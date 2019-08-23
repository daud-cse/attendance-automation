using System.ComponentModel.DataAnnotations;
using System.Web.Http.Filters;
using deepp.Entities.Models;
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
    // [RoutePrefix("api/department")]
    /// <summary>
    /// Department Controller
    /// </summary>
    public class DepartmentController : ApiController
    {
        #region "  -  [  Constructor  ]  -  "
        /// <summary>
        /// Gets or sets the department service.
        /// </summary>
        /// <value>
        /// The department service.
        /// </value>

        //private IDepartmentService DepartmentService { get; set; }
        private readonly IDepartmentService _departmentService;



        /// <summary>
        /// Gets or sets the unit of work asynchronous.
        /// </summary>
        /// <value>
        /// The unit of work asynchronous.
        /// </value>
        //private IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        /// <summary>
        /// Gets or sets the SPR service.
        /// </summary>
        /// <value>
        /// The SPR service.
        /// </value>
        private IStoredProcedureService SprService { get; set; }
        public DepartmentController(IDepartmentService departmentService, IStoredProcedureService sprService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _departmentService = departmentService;
            _unitOfWorkAsync = unitOfWorkAsync;
            SprService = sprService;

        }
        #endregion

        #region "  -  [  CRUD  ]  -  "


        // GET api/department
        public IEnumerable<Department> Get()
        {

            return _departmentService.GetDepartments(Sessions.InstituteId);


        }
         

        // GET api/department/5
        public Department Get(int id)
        {
            return _departmentService.GetDepartmentById(id);
        }

        // POST api/department
        [Validate]
        public HttpResponseMessage Post([FromBody]Department department)
        {

            department.InstituteId = Sessions.InstituteId;
            _departmentService.Insert(_unitOfWorkAsync, department);
 
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]Department department)
        {
            department.InstituteId = Sessions.InstituteId;
            _departmentService.Update(_unitOfWorkAsync, department);
  
        }

        // DELETE api/department/5
        public void Delete(int id)
        {

        }

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }
        #endregion
    }
}
