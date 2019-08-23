using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using deepp.erp;

namespace deepp.erp.Api
{
    public class RoleController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IRoleService _roleService;

        public RoleController(IUnitOfWorkAsync unitOfWorkAsync, IRoleService roleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _roleService = roleService;
        }

        // GET api/role
        public IEnumerable<Role> Get()
        {
            return _roleService.GetRoles(Sessions.InstituteId);
        }

        // GET api/role/5
        public Role Get(int id)
        {
            return _roleService.GetRoleById(id);
        }

        // POST api/role
        public HttpResponseMessage Post([FromBody]Role role)
        {
             //public int RoleId { get; set; }
        //public int RightId { get; set; }
            //role.RightsOfRoles
            role.InstituteId = Sessions.InstituteId;
            _roleService.Insert(_unitOfWorkAsync, role);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/role/5
        public void Put(int id, [FromBody]Role role)
        {
            role.InstituteId = Sessions.InstituteId;
            _roleService.Update(_unitOfWorkAsync, role);
        }

        // DELETE api/role/5
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
    }
}
