using deepp.erp;
using Microsoft.Practices.Unity;
using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.erp.Api
{
    public class AcademicGroupController : ApiController
    {
        [Dependency]
        public IAcademicGroupService AcademicGroupService { get; set; }
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
         
        
        // GET api/academicbranch
        public IEnumerable<AcademicGroup> Get(bool isActive = false)
        {            
            return AcademicGroupService.GetAcademicGroups(Sessions.InstituteId,isActive);
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
