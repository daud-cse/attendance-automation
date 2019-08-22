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
    public class GlobalDistrictController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGlobalDistrictService _globalDistrictService;


        public GlobalDistrictController(IUnitOfWorkAsync unitOfWorkAsync, IGlobalDistrictService  GlobalDistrictService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _globalDistrictService = GlobalDistrictService;
        }

        // GET api/GlobalDistrict
        public IEnumerable<GlobalDistrict> Get()
        {
            return _globalDistrictService.GetGlobalDistricts();
        }

        // GET api/GlobalDistrict/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/GlobalDistrict
        public HttpResponseMessage Post([FromBody]GlobalDistrict globalDistrict)
        {

            _globalDistrictService.Insert(_unitOfWorkAsync, globalDistrict);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/GlobalDistrict/5
        public void Put(int id, [FromBody]GlobalDistrict globalDistrict)
        {

            _globalDistrictService.Update(_unitOfWorkAsync, globalDistrict);
            
        }

        // DELETE api/GlobalDistrict/5
        public void Delete(int id)
        {
        }
    }
}
