using deepp.erp;
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
    public class GenderController : ApiController
    {
        private readonly IGenderService _genderService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public GenderController(IGenderService genderService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _genderService = genderService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/gender
        public IEnumerable<Gender> Get()
        {
            return _genderService.GetGenders(Sessions.InstituteId);
        }


        
        // GET api/gender/5
        public Gender Get(int id)
        {
            return _genderService.GetGenderById(id);
        }

        // POST api/gender
        [Validate]
        public HttpResponseMessage Post([FromBody]Gender gender)
        {
            gender.InstituteId = Sessions.InstituteId;
            _genderService.Insert(_unitOfWorkAsync, gender);
           
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]Gender gender)
        {
            gender.InstituteId = Sessions.InstituteId;
            _genderService.Update(_unitOfWorkAsync, gender);
    
        }

        // DELETE api/gender/5
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
