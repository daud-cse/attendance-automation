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
    public class AcademicClassController : ApiController
    {
 
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
         [Dependency]
        public IAcademicClassService  AcademicClassService { get; set; }
     
        public IEnumerable<AcademicClass> Get(bool isActive=false)
        {
            return AcademicClassService.GetAcademicClass(Sessions.InstituteId);
        }

        public AcademicClass Get(int id)
        {
            return AcademicClassService.GetAcademicClassById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicClass academicClass)
        {
            academicClass.InstituteId = Sessions.InstituteId;
            AcademicClassService.Insert(UnitOfWorkAsync,academicClass);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicClass academicClass)
        {
            academicClass.InstituteId = Sessions.InstituteId;
            AcademicClassService.Update(UnitOfWorkAsync, academicClass); 
        }

        // DELETE api/academicclass/5
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
