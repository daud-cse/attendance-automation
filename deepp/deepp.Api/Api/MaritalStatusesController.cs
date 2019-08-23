using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace deepp.Api.Api
{
   //  [RoutePrefix("api/maritalStatus")]



    public class MaritalStatusesController : ApiController
    {
          private readonly IMaritalStatusService _maritalStatusService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public MaritalStatusesController(IMaritalStatusService maritalStatusService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _maritalStatusService = maritalStatusService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/MaritalStatus
        public IEnumerable<MaritalStatus> Get( )
        {
            return _maritalStatusService.GetMaritalStatuss(Sessions.InstituteId);
        }

 
        // GET api/MaritalStatus/5
        public MaritalStatus Get(int id)
        {
            return _maritalStatusService.GetMaritalStatusById(id);
        }

        // POST api/MaritalStatus
        [Validate]
        public HttpResponseMessage Post([FromBody]MaritalStatus maritalStatus)
        {

            maritalStatus.InstituteId = Sessions.InstituteId;
            _maritalStatusService.Insert(_unitOfWorkAsync, maritalStatus);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/maritalStatus/5
        [Validate]
        public void Put(int id, [FromBody]MaritalStatus maritalStatus)
        {
            maritalStatus.InstituteId = Sessions.InstituteId;
            _maritalStatusService.Update(_unitOfWorkAsync, maritalStatus);
            
        }

        // DELETE api/MaritalStatus/5
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
