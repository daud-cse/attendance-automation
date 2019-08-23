using deepp.erp;
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
    public class AcademicVersionController : ApiController
    {
        private readonly IAcademicVersionService _academicVersionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AcademicVersionController(IAcademicVersionService academicVersionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicVersionService = academicVersionService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/academicbranch
        public IEnumerable<AcademicVersion> Get()
        {
            return _academicVersionService.GetAcademicVersions(Sessions.InstituteId);
        }


        
        public AcademicVersion Get(int id)
        {
            return _academicVersionService.GetAcademicVersionById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicVersion academicVersion)
        {

            academicVersion.InstituteId = Sessions.InstituteId;
            _academicVersionService.Insert(_unitOfWorkAsync, academicVersion);
      
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicVersion academicVersion)
        {

            academicVersion.InstituteId = Sessions.InstituteId;
            _academicVersionService.Update(_unitOfWorkAsync, academicVersion);
           

        }

        // DELETE api/academicbranch/5
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
