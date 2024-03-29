﻿using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace deepp.Api.Api
{
    public class ContactFeedbackController : ApiController
    {
        private readonly IContactUService _contactService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public ContactFeedbackController(
             IContactUService contactService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _contactService = contactService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        //[Route("api/contactfeedback/list")]
        //[HttpPost]
        //public VmSearch<ContactU> GetAllMobilePayments([FromBody]VmSearch<ContactU> mcontactListModel)
        //{

        //    mcontactListModel = mcontactListModel ?? new VmSearch<ContactU>();
        //    mcontactListModel.InstituteId = institutionId;
        //    IEnumerable<ContactU> list = _contactService.(mPaymentListModel);
        //    mPaymentListModel.SearchData = paymentlist;
        //    return mPaymentListModel;

        //}

        //[Route("api/contactfeedback/getsingle")]
        //public MobilePayment GetSingleNoticeById(int id)
        //{
        //    //MobilePayment mPaymentModel = _mPaymentService.GetMobilePaymentById(id);
        //    //mPaymentModel.studentPin = mPaymentModel.Student.UserInfo.PIN;
        //    //mPaymentModel.PaymentTypeList = _paymentTypeService.GetKVP();
        //    //return mPaymentModel;
        //}

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
