using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service.InstituteSubjects;
using pnsms.Service.Subjects;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api.Subjects
{
    public class InstituteSubjectController : ApiController
    {


          private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IInstituteSubjectService _InstituteSubjectService;


        public InstituteSubjectController(IUnitOfWorkAsync UnitOfWorkAsync
          , IInstituteSubjectService InstituteSubjectService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _InstituteSubjectService = InstituteSubjectService;

        }

        // GET api/Exam
        [Route("api/InstituteSubject/")]
        [HttpGet]
        public List<InstituteSubject> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
            var lstInstituteSubject= _InstituteSubjectService.Get(instituteid,true);
            return lstInstituteSubject;
        }

      
        // GET api/Exam
       

      
        // new student only
       // [Route("api/exam/newExam")]
        public InstituteSubjectClass GetNewExamModel()
        {
            var instituteid = Sessions.InstituteId;
            var SessionId=2;
            var objExam = new InstituteSubjectClass(); //_InstituteSubjectClassService.  (instituteid, SessionId);
            
            return objExam;
        }
        // POST api/Exam
         [Validate]
         [Route("api/InstituteSubject/")]
        public HttpResponseMessage Post([FromBody]List<InstituteSubject> lstInstituteSubject)
        {
           // objInstituteSubject.InstituteId = Sessions.InstituteId;
            _InstituteSubjectService.Insert(_UnitOfWorkAsync, lstInstituteSubject, Sessions.InstituteId);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5

        public void Put(int id, [FromBody]InstituteSubject objInstituteSubject)
        {
            objInstituteSubject.InstituteId = Sessions.InstituteId;
            _InstituteSubjectService.Update(_UnitOfWorkAsync, objInstituteSubject);
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
