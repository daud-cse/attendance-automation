using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace sfa.Api.Api.Settings
{
    public class AcademicClassController : ApiController
    {
 
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
         [Dependency]
        public IAcademicClassService  AcademicClassService { get; set; }

         [Route("api/AcademicClass/")]
         [HttpGet]
        public IEnumerable<AcademicClass> Get(int InstituteId)
        {
            return AcademicClassService.GetAcademicClass(InstituteId);
        }

        //public AcademicClass Get(int id)
        //{
        //    return AcademicClassService.GetAcademicClassById(id);
        //}

        // POST api/academicbranch
       
        public HttpResponseMessage Post([FromBody]AcademicClass academicClass)
        {
           // academicClass.InstituteId = Sessions.InstituteId;
            AcademicClassService.Insert(UnitOfWorkAsync,academicClass);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
       
        public void Put(int id, [FromBody]AcademicClass academicClass)
        {
            //academicClass.InstituteId = Sessions.InstituteId;
            AcademicClassService.Update(UnitOfWorkAsync, academicClass); 
        }

   
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
