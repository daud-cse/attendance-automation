using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api
{
    public class CertificatePrintTypeController : ApiController
    {
        private readonly ICertificatePrintTypeService _certificatePrintTypeService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private int instituteId = Sessions.InstituteId;


        public CertificatePrintTypeController(ICertificatePrintTypeService certificatePrintTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _certificatePrintTypeService = certificatePrintTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        //GET api/CertificatePrintType
        public IEnumerable<CertificatePrintType> Get()
        {
            return _certificatePrintTypeService.GetCertificatePrintTypes(instituteId, null);
        }


        // GET api/CertificatePrintType/5
        public CertificatePrintType Get(int id)
        {
            return _certificatePrintTypeService.GetCertificatePrintTypeById(id);
        }

        // POST api/CertificatePrintType
        [Validate]
        public HttpResponseMessage Post([FromBody]CertificatePrintType certificatePrintType)
        {

            certificatePrintType.InstituteId = instituteId;
            _certificatePrintTypeService.Insert(_unitOfWorkAsync, certificatePrintType);
      
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        [Validate]
        public void Put(int id, [FromBody]CertificatePrintType academicVersion)
        {

            academicVersion.InstituteId = instituteId;
            _certificatePrintTypeService.Update(_unitOfWorkAsync, academicVersion);
           

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
