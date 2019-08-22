using deepp.erp;
using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Service.Settings;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api.Setting
{
    public class AcademicPeriodController : ApiController
    {
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        [Dependency]
        public IAcademicPeriodService AcademicPeriodService { get; set; }

        public IEnumerable<AcademicPeriod> Get(bool isActive = false)
        {
            return AcademicPeriodService.GetAcademicPeriod(Sessions.InstituteId);
        }

        public AcademicPeriod Get(int id)
        {
            return AcademicPeriodService.GetAcademicPeriodById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicPeriod AcademicPeriod)
        {
            AcademicPeriod.InstituteId = Sessions.InstituteId;
            AcademicPeriodService.Insert(UnitOfWorkAsync, AcademicPeriod);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicPeriod AcademicPeriod)
        {
            AcademicPeriod.InstituteId = Sessions.InstituteId;
            AcademicPeriodService.Update(UnitOfWorkAsync, AcademicPeriod);
        }

        // DELETE api/AcademicPeriod/5
        public void Delete(int id)
        {
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
