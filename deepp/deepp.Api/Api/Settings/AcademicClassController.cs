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
    public class AcademicClassController : ApiController
    {

        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        [Dependency]
        public IAcademicClassService AcademicClassService { get; set; }
        [Dependency]
        public ISSOService _SSOService { get; set; }

        [Route("api/AcademicClass/")]
        public IEnumerable<AcademicClass> Get(bool isActive = false)
        {

            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                IEnumerable<AcademicClass> classList = new AcademicClass[] { new AcademicClass { Description = "Token Is not found" } };
                return classList;
            }
            else { return AcademicClassService.GetAcademicClass(objSSO.InstituteId); }
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
            AcademicClassService.Insert(UnitOfWorkAsync, academicClass);
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
