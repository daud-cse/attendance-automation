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
    public class CountryController : ApiController
    {
         private readonly ICountryService _countryService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public CountryController(ICountryService countryService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _countryService = countryService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/country
        public IEnumerable<Country> Get()
        {
            return _countryService.GetCountrys(Sessions.InstituteId);
        }

 
        // GET api/country/5
        public Country Get(int id)
        {
            return _countryService.GetCountryById(id);
        }

        // POST api/country
        [Validate]
        public HttpResponseMessage Post([FromBody]Country country)
        {
            country.InstituteId = Sessions.InstituteId;
            _countryService.Insert(_unitOfWorkAsync, country);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/country/5
        [Validate]
        public void Put(int id, [FromBody]Country country)
        {
            country.InstituteId = Sessions.InstituteId;
            _countryService.Update(_unitOfWorkAsync, country);
 
        }

        // DELETE api/country/5
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
