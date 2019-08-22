using deepp.erp;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api
{
    public class CertificatePrintController : ApiController
    {
        private readonly ICertificatePrintService _certificatePrintService;
        private readonly ICertificatePrintTypeService _certificatePrintTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public CertificatePrintController(
              ICertificatePrintService certificatePrintService
            , ICertificatePrintTypeService certificatePrintTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _certificatePrintService = certificatePrintService;
            _certificatePrintTypeService = certificatePrintTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/certificateprint/new")]
        [HttpPost]
        public CertificatePrint AddCertificatePrint()
        {
            var certificatePrintCreate = _certificatePrintService.AddNewCertificatePrint(institutionId);
            return certificatePrintCreate;
        }

        [Route("api/certificateprint/list")]
        [HttpPost]
        public VmSearch<CertificatePrint> GetAllCertificatePrints([FromBody]VmSearch<CertificatePrint> certificatePrintWithListModel)
        {

            certificatePrintWithListModel = certificatePrintWithListModel ?? new VmSearch<CertificatePrint>();
            certificatePrintWithListModel.InstituteId = institutionId;
            IEnumerable<CertificatePrint> certificatePrintlist = _certificatePrintService.GetAllCertificatePrint(institutionId);
            certificatePrintWithListModel.SearchData = certificatePrintlist;
            return certificatePrintWithListModel;

        }

        [Route("api/certificateprint/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CertificatePrint certificatePrintModel)
        {

            if (certificatePrintModel.Id == 0)
            {

                _certificatePrintService.SaveCertificatePrint(_unitOfWorkAsync, certificatePrintModel);
            }
            else
            {

                _certificatePrintService.UpdateCertificatePrint(_unitOfWorkAsync, certificatePrintModel);

            }
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/certificateprint/getsingle")]
        public CertificatePrint GetSingleCertificatePrintById(int id)
        {
            CertificatePrint certificatePrintModel = _certificatePrintService.GetCertificatePrintById(id, institutionId);
            return certificatePrintModel;
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
