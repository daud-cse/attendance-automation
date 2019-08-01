using pnsms.Entities.Models;
using pnsms.Entities.StoredProcedures.Models;
using pnsms.Entities.ViewModels;
using pnsms.erp;
using pnsms.Service.Exams;
using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace pnsms.Api.Api.Exams
{
    public class ExamController : ApiController
    {



        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IExamService _ExamService;
       

        public ExamController(IUnitOfWorkAsync UnitOfWorkAsync
          , IExamService ExamService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _ExamService = ExamService;

        }

        // GET api/Exam
        [Route("api/GetExam/")]
        [HttpGet]
        public IEnumerable<Exam> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
            var CurrentSessionId = Sessions.CurrentSessionId;

            var lstExamType= _ExamService.GetExamByInstituteId(instituteid);
         // var newExam=  _ExamService.newExam(instituteid, CurrentSessionId);

            return lstExamType;
        }

        [Route("api/GetExamById/")]
        [HttpGet]
        public Exam Get(int id)
        {
            return _ExamService.Find(id);
        }
        // GET api/Exam
        [Route("api/GetExamByCriteria/")]
        [HttpPost]
        public List<KeyValuePair<int, string>> Get(VmCommonSearch objVmCommonSearch)
        {
             objVmCommonSearch.InstituteId = Sessions.InstituteId;
             return _ExamService.GetKVP(objVmCommonSearch);
        }

      
        // new student only
        [Route("api/Exam/newExam")]
        [HttpGet]
        public Exam newExam(int id=0)
        {
            var instituteid = Sessions.InstituteId;
            int classid = id;
            var CurrentSessionId = Sessions.CurrentSessionId;
            var objExam = _ExamService.newExam(instituteid, CurrentSessionId, classid);
            
            return objExam;
        }
        // POST api/Exam
        [Route("api/SaveExam/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Exam Exam)
        {
            Exam.InstituteId = Sessions.InstituteId;
            _ExamService.Insert(_UnitOfWorkAsync, Exam);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5
        [Route("api/UpdateExam/")]
        [HttpPut]
        public void put(int id, [FromBody]Exam Exam)
        {
            Exam.InstituteId = Sessions.InstituteId;
            Exam.AcademicSessionId = Sessions.CurrentSessionId;
            _ExamService.Update(_UnitOfWorkAsync, Exam);
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
