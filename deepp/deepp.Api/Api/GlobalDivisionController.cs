using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deepp.Entities.Models;
using deepp.Service;
using deepp.Service.Institutes;
using Repository.Pattern.UnitOfWork;

namespace deepp.Api.Api
{
    public class GlobalDivisionController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGlobalDivisionService _globalDivisionService;


        public GlobalDivisionController(IUnitOfWorkAsync unitOfWorkAsync, IGlobalDivisionService  globalDivisionService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _globalDivisionService = globalDivisionService;
        }

        // GET api/GlobalDivision
        public IEnumerable<GlobalDivision> Get()
        {
            return _globalDivisionService.GetGlobalDivisions();
        }

        // GET api/GlobalDivision/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/GlobalDivision
        public HttpResponseMessage Post([FromBody]GlobalDivision globalDivision)
        {

            _globalDivisionService.Insert(_unitOfWorkAsync, globalDivision);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/GlobalDivision/5
        public void Put(int id, [FromBody]GlobalDivision globalDivision)
        {

            _globalDivisionService.Update(_unitOfWorkAsync, globalDivision);
            
        }

        // DELETE api/GlobalDivision/5
        public void Delete(int id)
        {
        }
    }
}
