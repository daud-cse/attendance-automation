using pnsms.Entities.Models;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace sfa.Api.Api.Exams
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

            int InstituteId = 5;           
            return _ExamGradeService.GetExamGradeByInstituteId(InstituteId);
        }

        public ExamGrade Get(int id)
        {
            return _ExamGradeService.Find(id);
        }
        // POST api/ExamGrade
        [Route("api/ExamGrade/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ExamGrade ExamGrade)
        {
           // ExamGrade.InstituteId = Sessions.InstituteId;
            int InstituteId = 5;
            _ExamGradeService.Insert(_UnitOfWorkAsync, ExamGrade);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/ExamGrade/5
       
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
