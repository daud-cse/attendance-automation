using pnsms.Entities.Models;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api.Exams
{
    public class ExamProcessController : ApiController
    {

        private readonly IUnitOfWorkAsync _unitOfWork;

        private readonly IStoredProcedures _storedProcedures;

        private readonly IExamProcessService _examProcessService;

        public ExamProcessController(IUnitOfWorkAsync unitOfWork
            , IStoredProcedures storedProcedures
            , IExamProcessService examProcessService
         )
        {
            _unitOfWork = unitOfWork;
            _storedProcedures = storedProcedures;
            _examProcessService = examProcessService;
        }

        [Route("api/newExamProcess/")]
        [HttpGet]
        public ExamProcess newExamProcess()
        {
            var InstituteId = Sessions.InstituteId;
            var userid = Sessions.UserId;
            var CurrentSessionId = Sessions.CurrentSessionId;

            return _examProcessService.newExamProcess(InstituteId, CurrentSessionId);
            
        }

        [Route("api/ExamProcess/")]
        [HttpPost]
        public string[] ExamProcess(ExamProcess objExamProcess)
        {
            var InstituteId = Sessions.InstituteId;
            var userid = Sessions.UserId;
            var CurrentSessionId= Sessions.CurrentSessionId;
            var msg= _storedProcedures.ExamProcess(InstituteId, CurrentSessionId, objExamProcess.AcademicClassesId, objExamProcess.ExamTypeId, userid);

            string[] starray = new string[] { msg };

            return starray;
        }

    }
}
