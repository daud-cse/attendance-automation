using deepp.erp;
using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.erp.Api
{
    public class MobilePaymentController : ApiController
    {
        private readonly IMobilePaymentService _mPaymentService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public MobilePaymentController(
              IMobilePaymentService mPaymentService
            , IPaymentTypeService paymentTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _mPaymentService = mPaymentService;
            _paymentTypeService = paymentTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("api/mobilepayment/new")]
        [HttpPost]
        public MobilePayment AddMobilePayment()
        {
            var mPaymentCreate = _mPaymentService.AddNewMobilePayment(institutionId);
            return mPaymentCreate;
        }


        [Route("api/mobilepayment/list")]
        [HttpPost]
        public VmSearch<MobilePayment> GetAllMobilePayments([FromBody]VmSearch<MobilePayment> mPaymentListModel)
        {

            mPaymentListModel = mPaymentListModel ?? new VmSearch<MobilePayment>();
            mPaymentListModel.InstituteId = institutionId;
            IEnumerable<MobilePayment> paymentlist = _mPaymentService.GetAllMobilePaymentListBySearch(mPaymentListModel);
            mPaymentListModel.SearchData = paymentlist;
            return mPaymentListModel;

        }


        [Route("api/mobilepayment/save")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]MobilePayment mPaymentModel)
        {
            bool status = true;
            mPaymentModel.LastActionBy = Sessions.UserId;
            if (mPaymentModel.Id == 0)
            {

               status= _mPaymentService.SaveMobilePayment(_unitOfWorkAsync, mPaymentModel);
            }
            else
            {

               status = _mPaymentService.UpdateMobilePayment(_unitOfWorkAsync, mPaymentModel);

            }
            if (status) { return new HttpResponseMessage(HttpStatusCode.Created); }
            else
            {
               
                var ss = new HttpResponseMessage();
                
                ss.ReasonPhrase = "<h3>Please Submit Correct Input Values</h3>";
                ss.StatusCode = HttpStatusCode.Conflict;


                return ss;// new HttpResponseMessage(HttpStatusCode.da);
            }
        }

        [Route("api/mobilepayment/getsingle")]
        public MobilePayment GetSingleNoticeById(int id)
        {
            MobilePayment mPaymentModel = _mPaymentService.GetMobilePaymentById(id);
            mPaymentModel.studentPin = mPaymentModel.Student.UserInfo.PIN;
            mPaymentModel.PaymentTypeList = _paymentTypeService.GetKVP();
            return mPaymentModel;
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
