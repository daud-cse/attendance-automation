using pnsms.Entities.Models;
using pnsms.Service.Accounts;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api.Vendor
{
    public class VendorInfoController : ApiController
    {

        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IVendorInfoService _VendorInfoService;


        public VendorInfoController(IUnitOfWorkAsync UnitOfWorkAsync
          , IVendorInfoService VendorInfoService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _VendorInfoService = VendorInfoService;

        }
        // new student only
        [Route("api/VendorInfo/newVendorInfo")]
        [HttpGet]
        public VendorInfo newVendorInfo(int id=0)
        {
            var instituteid = Sessions.InstituteId;

            var objBankAccountInfo = _VendorInfoService.newVendorInfo(instituteid);

            return objBankAccountInfo;
        }

        // GET api/Exam
        [Route("api/GetVendorAccount/")]
        [HttpGet]
        public IEnumerable<VendorInfo> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
           

            var ObjVendorTypeId = _VendorInfoService.GetAllVendorInfo(instituteid);
            // var newExam=  _ExamService.newExam(instituteid, CurrentSessionId);

            return ObjVendorTypeId;
        }

        // POST api/BankAccount
        [Route("api/SaveVendorAccount/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]VendorInfo VendorInfo)
        {
            VendorInfo.InstituteId = Sessions.InstituteId;
            _VendorInfoService.Insert(_UnitOfWorkAsync, VendorInfo);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
       

        // PUT api/Exam/5
        [Route("api/UpdateVendorAccount/")]
        [HttpPut]
        public void put(int id, [FromBody]VendorInfo VendorInfo)
        {
            VendorInfo.InstituteId = Sessions.InstituteId;

            _VendorInfoService.Update(_UnitOfWorkAsync, VendorInfo);
        }

        // DELETE api/Exam/5
        public void Delete(int id)
        {
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
