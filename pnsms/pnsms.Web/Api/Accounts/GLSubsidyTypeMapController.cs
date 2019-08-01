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

   
    public class GLSubsidyTypeMapController : ApiController
    {

        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IFMS_GLSubsidyTypeMapService _FMS_GLSubsidyTypeMapService;
        private readonly IFMS_GLAccountService _FMS_GLAccountService;

        public GLSubsidyTypeMapController(IUnitOfWorkAsync UnitOfWorkAsync
         , IFMS_GLSubsidyTypeMapService FMS_GLSubsidyTypeMapService,
            IFMS_GLAccountService  FMS_GLAccountService
        )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _FMS_GLSubsidyTypeMapService = FMS_GLSubsidyTypeMapService;
            _FMS_GLAccountService = FMS_GLAccountService;

        }

        // new student only
        [Route("api/GLSubsidyTypeInfo/newGLSubsidyTypeInfo")]
        [HttpGet]
        public FMS_GLSubsidyTypeMap newGLSubsidyTypeInfo(int id = 0)
        {
            var instituteid = Sessions.InstituteId;


            var objGLSubsidyAccountInfo = _FMS_GLSubsidyTypeMapService.newFMS_GLSubsidyTypeMap(instituteid);

            return objGLSubsidyAccountInfo;
        }

        // GET api/Exam
        [Route("api/GetGLSubsidyTypeInfo/")]
        [HttpGet]
        public IEnumerable<FMS_GLSubsidyTypeMap> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
            var CurrentSessionId = Sessions.CurrentSessionId;

            var ObjBankTypeId = _FMS_GLSubsidyTypeMapService.GetAllFMS_GLSubsidyTypeMap(instituteid);
            // var newExam=  _ExamService.newExam(instituteid, CurrentSessionId);

            return ObjBankTypeId;
        }

        // POST api/BankAccount
        [Route("api/SaveGLSubsidyTypeInfo/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FMS_GLSubsidyTypeMap FMS_GLSubsidyTypeMap)
        {
            FMS_GLSubsidyTypeMap.InstituteId = Sessions.InstituteId;
            FMS_GLSubsidyTypeMap.SetBy = "Rubel";
            //    BankAccountInfo.InstituteId=
            _FMS_GLSubsidyTypeMapService.Insert(_UnitOfWorkAsync, FMS_GLSubsidyTypeMap);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5
        [Route("api/UpdateGLSubsidyTypeInfo/")]
        [HttpPut]
        public void put(int id, [FromBody]FMS_GLSubsidyTypeMap FMS_GLSubsidyTypeMap)
        {


            FMS_GLSubsidyTypeMap.InstituteId = Sessions.InstituteId;
            FMS_GLSubsidyTypeMap.SetBy = Sessions.UserId.ToString();
            _FMS_GLSubsidyTypeMapService.Update(_UnitOfWorkAsync, FMS_GLSubsidyTypeMap);
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
