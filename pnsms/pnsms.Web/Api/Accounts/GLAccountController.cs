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
    public class GLAccountController : ApiController
    {
        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IFMS_GLAccountService _GLAccountInfoService;


        public GLAccountController(IUnitOfWorkAsync UnitOfWorkAsync
          , IFMS_GLAccountService GLAccountInfoService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _GLAccountInfoService = GLAccountInfoService;

        }
        // new student only
        //[Route("api/GLAccountInfo/newGLAccountInfo")]
        //[HttpGet]
        //public FMS_GLAccount newGLAccountInfo(int id = 0)
        //{
        //    var instituteid = Sessions.InstituteId;

        //    var objBankAccountInfo = _GLAccountInfoService.newVendorInfo(instituteid);

        //    return objBankAccountInfo;
        //}

        // GET api/Exam
        [Route("api/GetGLAccount/")]
        [HttpGet]
        public IEnumerable<FMS_GLAccount> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;


            var ObjVendorTypeId = _GLAccountInfoService.GetAllFMS_GLAccount(instituteid).OrderBy(x => x.GLAccountCode);
            // var newExam=  _ExamService.newExam(instituteid, CurrentSessionId);

            return ObjVendorTypeId;
        }

        //// POST api/BankAccount
        [Route("api/SaveGLAccount/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FMS_GLAccount FMS_GLAccount)
        {
            FMS_GLAccount.InstituteId = Sessions.InstituteId;
            //    BankAccountInfo.InstituteId=
            _GLAccountInfoService.Insert(_UnitOfWorkAsync, FMS_GLAccount);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }


        //// PUT api/Exam/5
        [Route("api/UpdateGLAccount/")]
        [HttpPut]
        public void put(int id, [FromBody]FMS_GLAccount FMS_GLAccount)
        {
            FMS_GLAccount.InstituteId = Sessions.InstituteId;

            _GLAccountInfoService.Update(_UnitOfWorkAsync, FMS_GLAccount);
        }

        //// DELETE api/Exam/5
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
