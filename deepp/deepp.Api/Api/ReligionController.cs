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

namespace deepp.Api.Api
{
    public class ReligionController : ApiController
    {
       private readonly IReligionService _religionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public ReligionController(IReligionService religionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _religionService = religionService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/religion
        public IEnumerable<Religion> Get()
        {
            return _religionService.GetReligions(Sessions.InstituteId);
        } 
        
        // GET api/religion/5
        public Religion Get(int id)
        {
            return _religionService.GetReligionById(id);
        }

        // POST api/religion
        [Validate]
        public HttpResponseMessage Post([FromBody]Religion religion)
        {
            religion.InstituteId = Sessions.InstituteId;
            _religionService.Insert(_unitOfWorkAsync , religion);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/religion/5
        [Validate]
        public void Put(int id, [FromBody]Religion religion)
        {
            religion.InstituteId = Sessions.InstituteId;
            _religionService.Update(_unitOfWorkAsync, religion);
       

        }

        // DELETE api/religion/5
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
