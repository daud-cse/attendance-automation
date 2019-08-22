using pnsms.Entities.Models;
using pnsms.Entities.StoredProcedures.Models;
using pnsms.Service.Exams;

using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using sfa.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace sfa.Api.Api.Exams
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
        [Route("api/Exam/")]
        [HttpGet]
        public IEnumerable<Exam> Get(int instituteid, bool isActive = false)
        {
            return _ExamService.GetExamByInstituteId(instituteid);
        }

        public Exam Get(int id)
        {
            return _ExamService.Find(id);
        }


        // new student only
        [Route("api/exam/newExam")]
        public Exam GetNewExamModel()
        {
            var InstituteId = 5;
            var SessionId=2;
            var objExam = _ExamService.newExam(InstituteId, SessionId,3);
            return objExam;
        }
        // POST api/Exam
       
        public HttpResponseMessage Post([FromBody]Exam Exam)
        {
            Exam.InstituteId = Sessions.InstituteId;
            _ExamService.Insert(_UnitOfWorkAsync, Exam);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5
       
        public void Put(int id, [FromBody]Exam Exam)
        {
            Exam.InstituteId = Sessions.InstituteId;
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
