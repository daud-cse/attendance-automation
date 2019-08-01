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
    public class BankAccountInfoController : ApiController
    {

        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IBankAccountInfoService _BankAccountInfoService;


        public BankAccountInfoController(IUnitOfWorkAsync UnitOfWorkAsync
          , IBankAccountInfoService BankAccountInfoService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _BankAccountInfoService = BankAccountInfoService;

        }
        // new student only
        [Route("api/BankAccountInfo/newBankAccountInfo")]
        [HttpGet]
        public BankAccountInfo newBankAccountInfo(int id = 0)
        {
            var instituteid = Sessions.InstituteId;

            var objBankAccountInfo = _BankAccountInfoService.newBankAccountInfo(instituteid);

            return objBankAccountInfo;
        }

        // GET api/Exam
        [Route("api/GetBankAccount/")]
        [HttpGet]
        public IEnumerable<BankAccountInfo> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
            var CurrentSessionId = Sessions.CurrentSessionId;

            var ObjBankTypeId = _BankAccountInfoService.GetAllBankAccountInfo(instituteid);
            // var newExam=  _ExamService.newExam(instituteid, CurrentSessionId);

            return ObjBankTypeId;
        }

        // POST api/BankAccount
        [Route("api/SaveBankAccount/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]BankAccountInfo BankAccountInfo)
        {
            BankAccountInfo.InstituteId = Sessions.InstituteId;
        //    BankAccountInfo.InstituteId=
            _BankAccountInfoService.Insert(_UnitOfWorkAsync, BankAccountInfo);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5
        [Route("api/UpdateBankAccount/")]
        [HttpPut]
        public void put(int id, [FromBody]BankAccountInfo BankAccountInfo)
        {

           
            BankAccountInfo.InstituteId = Sessions.InstituteId;
            BankAccountInfo.SetBy = Sessions.UserId.ToString();
            _BankAccountInfoService.Update(_UnitOfWorkAsync, BankAccountInfo);
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
