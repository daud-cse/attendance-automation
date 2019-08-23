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
    public class DistrictOrStateController : ApiController
    {
        private readonly IDistrictOrStateService _districtOrStateService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public DistrictOrStateController(IDistrictOrStateService districtOrStateService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _districtOrStateService = districtOrStateService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/DistrictOrState
        public IEnumerable<DistrictOrState> Get(bool IsActive = false)
        {
            return _districtOrStateService.GetDistrictOrStates(IsActive);
        }


       
        // GET api/DistrictOrState/5
        public DistrictOrState Get(int id)
        {
            return _districtOrStateService.GetDistrictOrStateById(id);
        }

        // POST api/DistrictOrState
        [Validate]
        public HttpResponseMessage Post([FromBody]DistrictOrState districtOrState)
        {
            _districtOrStateService.Insert(_unitOfWorkAsync, districtOrState);
 
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]DistrictOrState districtOrState)
        {
 
            _districtOrStateService.Update(_unitOfWorkAsync, districtOrState);
     

        }

        // DELETE api/DistrictOrState/5
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
