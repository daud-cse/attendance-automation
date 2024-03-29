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
    public class ContactUsFeedbackController : ApiController
    {
        private readonly IContactUService _contactusService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        int institutionId = Sessions.InstituteId;

        public ContactUsFeedbackController(
              IContactUService contactusService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _contactusService = contactusService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        [Route("api/contactusfeedback/list")]
        [HttpPost]
        public VmSearch<ContactU> GetAllCertificatePrints([FromBody]VmSearch<ContactU> contactFeedbackModel)
        {

            contactFeedbackModel = contactFeedbackModel ?? new VmSearch<ContactU>();
            contactFeedbackModel.InstituteId = institutionId;
            IEnumerable<ContactU> certificatePrintlist = _contactusService.GetAllBySearch(contactFeedbackModel);
            contactFeedbackModel.SearchData = certificatePrintlist;
            return contactFeedbackModel;

        }


        [Route("api/contactusfeedback/getsingle")]
        public ContactU GetSingleById(int id)
        {
            ContactU contactFeedbackModel = _contactusService.GetContactUSById(id);
            return contactFeedbackModel;
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
