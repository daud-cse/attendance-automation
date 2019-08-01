using pnsms.Entities.Models;
using pnsms.erp;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace pnsms.Api.Api.Exams
{
    public class ExamGradeController : ApiController
    {

        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IExamGradeService _ExamGradeService;


        public ExamGradeController(IUnitOfWorkAsync UnitOfWorkAsync
          , IExamGradeService ExamGradeService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _ExamGradeService = ExamGradeService;

        }



        // GET api/ExamGrade
        [Route("api/ExamGrade/")]
        [HttpGet]
        public IEnumerable<ExamGrade> Get(bool isActive = false)
        {

                
            return _ExamGradeService.GetExamGradeByInstituteId(Sessions.InstituteId).OrderByDescending(x=>x.GradePoint);
        }

        public ExamGrade Get(int id)
        {
            return _ExamGradeService.Find(id);
        }
        // POST api/ExamGrade
        [Route("api/SaveExamGrade/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ExamGrade objExamGrade)
        {
            objExamGrade.InstituteId = Sessions.InstituteId;
            _ExamGradeService.Insert(_UnitOfWorkAsync, objExamGrade);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/UpdateExamGrade/")]
        [HttpPut]
        public void Put(int id, [FromBody]ExamGrade ExamGrade)
        {
            ExamGrade.InstituteId = Sessions.InstituteId;
            _ExamGradeService.Update(_UnitOfWorkAsync, ExamGrade);
        }

        // DELETE api/ExamGrade/5
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
