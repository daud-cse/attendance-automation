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
    public class FeesHeadController : ApiController
    {
        private readonly IFeesHeadService _feesHeadService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public FeesHeadController(IFeesHeadService feesHeadService, IUnitOfWorkAsync unitOfWorkAsync)

        {
            this._feesHeadService = feesHeadService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/saveFeeshead")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FeesHead p_feesHead)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    p_feesHead.InstituteId = Sessions.InstituteId;                    
                    p_feesHead.LastUpdateTime = DateTime.Now;
                    _feesHeadService.Insert(p_feesHead);
                    _unitOfWorkAsync.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/editFeeshead")]
       
        public HttpResponseMessage Put(int id,[FromBody]FeesHead p_feesHead)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    p_feesHead.InstituteId = Sessions.InstituteId;
                    p_feesHead.LastUpdateTime = DateTime.Now;
                    _feesHeadService.Update(p_feesHead);
                    _unitOfWorkAsync.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/feeshead/")]
        [HttpGet]
        public IEnumerable<FeesHead> Get()
        {
            var feesList = _feesHeadService.GetFeesHeads(Sessions.InstituteId, null);
            return feesList;

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
