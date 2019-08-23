using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;

namespace deepp.Api.Api
{
    public class ScholarshipController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IScholarshipService _scholarshipService;

        public ScholarshipController(IUnitOfWorkAsync unitOfWorkAsync, IScholarshipService scholarshipService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _scholarshipService = scholarshipService;
        }

        // GET api/Scholarship
        public IEnumerable<Scholarship> Get()
        {
            return _scholarshipService.GetScholarshipByInstituteId(Sessions.InstituteId);
        }

        // GET api/Scholarship/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Scholarship
        public HttpResponseMessage Post([FromBody]Scholarship scholarship)
        {
            scholarship.InstituteId = Sessions.InstituteId;
            _scholarshipService.Insert(_unitOfWorkAsync, scholarship);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/Scholarship/5
        public void Put(int id, [FromBody]Scholarship scholarship)
        {
            scholarship.InstituteId = Sessions.InstituteId;
            _scholarshipService.Update(_unitOfWorkAsync, scholarship);
            
        }

        // DELETE api/Scholarship/5
        public void Delete(int id)
        {
        }
    }
}
