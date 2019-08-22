using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using deepp.erp;

namespace pnsms.erp.Api
{
    public class CoCurricularActivityController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICoCurricularActivityService _coCurricularActivityService;

        public CoCurricularActivityController(IUnitOfWorkAsync unitOfWorkAsync, ICoCurricularActivityService coCurricularActivityService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _coCurricularActivityService = coCurricularActivityService;
        }

        // GET api/cocurricularactivity
        public IEnumerable<CoCurricularActivity> Get()
        {
            return _coCurricularActivityService.GetCoCurricularActivityByInstituteId(Sessions.InstituteId);
        }

        // GET api/cocurricularactivity/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/cocurricularactivity
        public HttpResponseMessage Post([FromBody]CoCurricularActivity coCurricularActivity)
        {
            coCurricularActivity.InstituteId = Sessions.InstituteId;
            _coCurricularActivityService.Insert(_unitOfWorkAsync, coCurricularActivity);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/cocurricularactivity/5
        public void Put(int id, [FromBody]CoCurricularActivity coCurricularActivity)
        {
            coCurricularActivity.InstituteId = Sessions.InstituteId;
            _coCurricularActivityService.Update(_unitOfWorkAsync, coCurricularActivity);
            
        }

        // DELETE api/cocurricularactivity/5
        public void Delete(int id)
        {
        }
    }
}
