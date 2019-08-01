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
    public class GlobalInstituteTypeController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGlobalInstituteTypeService _globalInstituteTypeService;


        public GlobalInstituteTypeController(IUnitOfWorkAsync unitOfWorkAsync, IGlobalInstituteTypeService  globalInstituteTypeService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _globalInstituteTypeService = globalInstituteTypeService;
        }

        // GET api/GlobalInstituteType
        public IEnumerable<GlobalInstituteType> Get()
        {
            return _globalInstituteTypeService.GetGlobalInstituteTypes();
        }

        // GET api/GlobalInstituteType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/GlobalInstituteType
        public HttpResponseMessage Post([FromBody]GlobalInstituteType globalInstituteType)
        {

            _globalInstituteTypeService.Insert(_unitOfWorkAsync, globalInstituteType);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/GlobalInstituteType/5
        public void Put(int id, [FromBody]GlobalInstituteType globalInstituteType)
        {

            _globalInstituteTypeService.Update(_unitOfWorkAsync, globalInstituteType);
            
        }

        // DELETE api/GlobalInstituteType/5
        public void Delete(int id)
        {
        }
    }
}
