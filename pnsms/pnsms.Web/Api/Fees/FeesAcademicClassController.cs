using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Entities.ViewModels.Fees;
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
    [Route("api/feesacademicclass/")]
    public class FeesAcademicClassController : ApiController
    {
        private readonly IFeesAcademicClassService _feesAcademicClassService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public FeesAcademicClassController(IFeesAcademicClassService feesAcademicClassService, IUnitOfWorkAsync unitOfWorkAsync)

        {
            this._feesAcademicClassService = feesAcademicClassService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/feesacademicclass/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FeesHead p_feesHead)
        {
            if (ModelState.IsValid)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("saves")]
        [HttpPost]
        public HttpResponseMessage saves([FromBody]List<FeesAcademicClass> p_feesAcademicClass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (FeesAcademicClass fac in p_feesAcademicClass)
                    {
                        fac.InstituteId = institutionId;
                        fac.IsActive = true;
                        fac.LastUpdateTime = DateTime.Now;
                        _feesAcademicClassService.Insert(fac);
                    }
                    _unitOfWorkAsync.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }


        [Route("api/feesacademicclass/edit")]
        [HttpPost]
        public HttpResponseMessage Put([FromBody]List<VMFeesAcademicClass> p_feesAcademicClass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _feesAcademicClassService.saves(p_feesAcademicClass, _unitOfWorkAsync);
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
                }

            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // GET: api/feesacademicclass
        [HttpGet]
        [Route("api/feesacademicclass/get")]
        public List<VMFeesAcademicClass> Get()
        {
            var feesAcademicClassList = _feesAcademicClassService.Get(Sessions.InstituteId);
            return feesAcademicClassList;

        }
        [HttpGet]
        [Route("api/feesacademicclass/getReport")]
        public List<VMFeesAcademicClass> getReport(string p_selectedOption, int p_feesAcademicClassId)
        {
            var feesAcademicClassList = _feesAcademicClassService.Get(Sessions.InstituteId);
            return feesAcademicClassList;

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
