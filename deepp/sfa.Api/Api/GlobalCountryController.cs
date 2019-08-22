using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.Service.Institutes;
using Repository.Pattern.UnitOfWork;

namespace sfa.Api.Api
{
    public class GlobalCountryController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGlobalCountryService _globalCountryService;


        public GlobalCountryController(IUnitOfWorkAsync unitOfWorkAsync, IGlobalCountryService  globalCountryService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _globalCountryService = globalCountryService;
        }

        // GET api/globalCountry
        public IEnumerable<GlobalCountry> Get()
        {
            return _globalCountryService.GetGlobalCountries();
        }

        // GET api/globalCountry/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/globalCountry
        public HttpResponseMessage Post([FromBody]GlobalCountry globalCountry)
        {

            _globalCountryService.Insert(_unitOfWorkAsync, globalCountry);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/globalCountry/5
        public void Put(int id, [FromBody]GlobalCountry globalCountry)
        {

            _globalCountryService.Update(_unitOfWorkAsync, globalCountry);
            
        }

        // DELETE api/globalCountry/5
        public void Delete(int id)
        {
        }
    }
}
