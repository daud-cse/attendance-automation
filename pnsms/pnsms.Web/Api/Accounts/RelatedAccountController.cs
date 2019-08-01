using pnsms.Entities.Models;
using pnsms.Service.Accounts;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api.Accounts
{
    public class RelatedAccountController : ApiController
    {

        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IFMS_RelatedAccountService _FMS_RelatedAccountService;


        public RelatedAccountController(IUnitOfWorkAsync UnitOfWorkAsync
          , IFMS_RelatedAccountService FMS_RelatedAccountService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _FMS_RelatedAccountService = FMS_RelatedAccountService;

        }
        // new student only
        [Route("api/RelatedAccountInfo/newRelatedAccountInfo")]
        [HttpGet]
        public FMS_RelatedAccount newFMS_RelatedAccountInfo(int id = 0)
        {
            var instituteid = Sessions.InstituteId;

            var objRelatedAccountInfo = _FMS_RelatedAccountService.newFMS_RelatedAccountInfo(instituteid);

            return objRelatedAccountInfo;
        }

        // GET api/Exam
        [Route("api/GetRelatedAccount/")]
        [HttpGet]
        public IEnumerable<FMS_RelatedAccount> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
            var CurrentSessionId = Sessions.CurrentSessionId;

            var ObjRelatedAccountTypeId = _FMS_RelatedAccountService.GetAllFMS_RelatedAccountInfo(instituteid);
            // var newExam=  _ExamService.newExam(instituteid, CurrentSessionId);

            return ObjRelatedAccountTypeId;
        }

        // POST api/BankAccount
        [Route("api/SaveRelatedAccount/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FMS_RelatedAccount FMS_RelatedAccount)
        {
            FMS_RelatedAccount.InstituteId = Sessions.InstituteId;
            //    BankAccountInfo.InstituteId=
            _FMS_RelatedAccountService.Insert(_UnitOfWorkAsync, FMS_RelatedAccount);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5
        [Route("api/UpdateRelatedAccount/")]
        [HttpPut]
        public void put(int id, [FromBody]FMS_RelatedAccount FMS_RelatedAccount)
        {


            FMS_RelatedAccount.InstituteId = Sessions.InstituteId;

            _FMS_RelatedAccountService.Update(_UnitOfWorkAsync, FMS_RelatedAccount);
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
