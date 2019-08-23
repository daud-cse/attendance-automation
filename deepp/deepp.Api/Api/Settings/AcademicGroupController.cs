using Microsoft.Practices.Unity;
using deepp.Entities.Models;
using deepp.Service;
using deepp.Service.SSOLogin;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.Api.Api.Settings
{
    public class AcademicGroupController : ApiController
    {
        [Dependency]
        public IAcademicGroupService AcademicGroupService { get; set; }
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }

         [Dependency]
        public ISSOService _SSOService { get; set; }

        [Route("api/AcademicGroup")]
        public IEnumerable<AcademicGroup> Get(bool isActive = false)
        {
           
            isActive = true;
            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
             {
                 IEnumerable<AcademicGroup> AcademicGroupList = new AcademicGroup[] { new AcademicGroup { Name = "Token Is not found" } };
                 return AcademicGroupList;
             }
             else
             {
                 return AcademicGroupService.GetAcademicGroups(objSSO.InstituteId, isActive);
             }
           
        }

        public AcademicGroup Get(int id)
        {
            return AcademicGroupService.GetGetAcademicGroupById(id);
        }
        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicGroup academicGroup)
        {
            academicGroup.InstituteId = Sessions.InstituteId;
            AcademicGroupService.Insert(UnitOfWorkAsync,academicGroup);            
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicGroup academicGroup)
        {
            academicGroup.InstituteId = Sessions.InstituteId;
            AcademicGroupService.Update(UnitOfWorkAsync,academicGroup);
        }

        // DELETE api/academicbranch/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }
    }
}
