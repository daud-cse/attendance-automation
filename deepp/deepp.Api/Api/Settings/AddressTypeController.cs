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
    public class AddressTypeController : ApiController
    {
        private readonly IAddressTypeService _addressTypeService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AddressTypeController(IAddressTypeService addressTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _addressTypeService = addressTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/addressType
        public IEnumerable<AddressType> Get()
        {
            return _addressTypeService.GetAddressTypes(Sessions.InstituteId);
        }


        // GET api/addressType/5
        public AddressType Get(int id)
        {
            return _addressTypeService.GetAddressTypeById(id);
        }

        // POST api/addressType
        [Validate]
        public HttpResponseMessage Post([FromBody]AddressType addressType)
        {
            addressType.InstituteId = Sessions.InstituteId;
            _addressTypeService.Insert(_unitOfWorkAsync, addressType);

            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/addressType/5
        [Validate]
        public void Put(int id, [FromBody]AddressType addressType)
        {
            addressType.InstituteId = Sessions.InstituteId;
            _addressTypeService.Update(_unitOfWorkAsync, addressType);

        }

        // DELETE api/addressType/5
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
