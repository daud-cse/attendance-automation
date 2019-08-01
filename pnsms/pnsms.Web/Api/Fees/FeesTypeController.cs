using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service;
using pnsms.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    public class FeesTypeController : ApiController
    {
        private readonly IFeesTypeService _feesTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public FeesTypeController(IFeesTypeService feesTypeService, IUnitOfWorkAsync unitOfWorkAsync)

        {
            this._feesTypeService = feesTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/saveFeesType")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FeesType p_feesType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    p_feesType.InstituteId = Sessions.InstituteId;
                    p_feesType.IsActive = true;
                    p_feesType.LastUpdateTime = DateTime.Now;
                    _feesTypeService.Insert(p_feesType);
                    _unitOfWorkAsync.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/editFeesType")]
       
        public HttpResponseMessage Put(int id,[FromBody]FeesType p_feesType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    p_feesType.InstituteId = Sessions.InstituteId;
                    p_feesType.LastUpdateTime = DateTime.Now;
                    _feesTypeService.Update(p_feesType);
                    _unitOfWorkAsync.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/feesType")]
        [HttpGet]
        public List<FeesType> Get()
        {
            var feesTypeList = _feesTypeService.GetFeesTypes(Sessions.InstituteId, true).ToList();
            return feesTypeList;

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
