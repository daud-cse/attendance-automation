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

namespace deepp.Api.Api.Settings
{
    public class BloodGroupController : ApiController
    {
        private readonly IBloodGroupService _bloodGroupService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public BloodGroupController(IBloodGroupService bloodGroupService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bloodGroupService = bloodGroupService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/bloodGroup
        public IEnumerable<BloodGroup> Get()
        {
            return _bloodGroupService.GetBloodGroups(Sessions.InstituteId);
        }

        // GET api/bloodGroup/5
        public BloodGroup Get(int id)
        {
            return _bloodGroupService.GetBloodGroupById(id);
        }

        // POST api/bloodGroup
        [Validate]
        public HttpResponseMessage Post([FromBody]BloodGroup bloodGroup)
        {
            bloodGroup.InstituteId = Sessions.InstituteId;

            _bloodGroupService.Insert(_unitOfWorkAsync, bloodGroup);

            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]BloodGroup bloodGroup)
        {
            bloodGroup.InstituteId = Sessions.InstituteId;
            _bloodGroupService.Update(_unitOfWorkAsync, bloodGroup);


        }

        // DELETE api/bloodGroup/5
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
