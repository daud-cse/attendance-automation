using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api
{
    public class OnlineAdmissionController : ApiController
    {
        private readonly IVmOnlineAdmissionService _vmOnlineAdmissionService;
        private readonly IAdmissionFormService _onlineAdmissionService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public OnlineAdmissionController(
              IVmOnlineAdmissionService vmOnlineAdmissionService
            , IAdmissionFormService onlineAdmissionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _vmOnlineAdmissionService = vmOnlineAdmissionService;
            _onlineAdmissionService = onlineAdmissionService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/onlineadmission/list")]
        [HttpPost]
        public VmSearch<AdmissionForm> GetAllList([FromBody]VmSearch<AdmissionForm> admissionFormListModel)
        {

            admissionFormListModel = admissionFormListModel ?? new VmSearch<AdmissionForm>();
            admissionFormListModel.InstituteId = institutionId;
            return _vmOnlineAdmissionService.GetAllList(admissionFormListModel);

        }

        [Route("api/onlineadmission/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]AdmissionForm admissionFormModel)
        {
            if (!admissionFormModel.IsSelected) { admissionFormModel.IsActive = false; }
            _onlineAdmissionService.Update(_unitOfWorkAsync, admissionFormModel);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/onlineadmission/update")]
        [HttpPost]
        public HttpResponseMessage Update([FromBody]AdmissionForm admissionFormModel)
        {
            if (!admissionFormModel.StatusDeatails)
            { admissionFormModel.IsActive = false; admissionFormModel.IsSelected = false; }
            else { admissionFormModel.IsActive = true; admissionFormModel.IsSelected = false; }
            _onlineAdmissionService.Update(_unitOfWorkAsync, admissionFormModel);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }


        [Route("api/onlineadmission/getsingle/")]
        public VmOnlineAdmission GetApplicantById(int id)
        {
            VmOnlineAdmission applicantDetailsModel = _vmOnlineAdmissionService.GetApplicantById(id);
            return applicantDetailsModel;
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
