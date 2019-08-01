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

namespace pnsms.erp.Api
{
    public class NationalityController : ApiController
    {
        private readonly INationalityService _nationalityService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public NationalityController(INationalityService nationalityService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _nationalityService = nationalityService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/Nationality
        public IEnumerable<Nationality> Get()
        {
            return _nationalityService.GetNationalitys(Sessions.InstituteId);
        }


       
        // GET api/Nationality/5
        public Nationality Get(int id)
        {
            return _nationalityService.GetNationalityById(id);
        }

        // POST api/Nationality
        [Validate]
        public HttpResponseMessage Post([FromBody]Nationality nationality)
        {

            nationality.InstituteId = Sessions.InstituteId;
            _nationalityService.Insert(_unitOfWorkAsync, nationality);
 
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/nationality/5
        [Validate]
        public void Put(int id, [FromBody]Nationality nationality)
        {
            nationality.InstituteId = Sessions.InstituteId;
            _nationalityService.Update(_unitOfWorkAsync, nationality);

        }

        // DELETE api/nationality/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
