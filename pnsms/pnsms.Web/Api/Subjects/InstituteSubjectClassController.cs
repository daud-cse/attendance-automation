using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service.Subjects;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pnsms.erp.Api.Subjects
{
    public class InstituteSubjectClassController : ApiController
    {

        
        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IInstituteSubjectClassService _InstituteSubjectClassService;


        public InstituteSubjectClassController(IUnitOfWorkAsync UnitOfWorkAsync
          , IInstituteSubjectClassService InstituteSubjectClassService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _InstituteSubjectClassService = InstituteSubjectClassService;

        }

        // GET api/Exam
        [Route("api/InstituteSubjectClass/")]
        [HttpGet]
        public IEnumerable<InstituteSubjectClass> Get(bool isActive = false)
        {
            var instituteid = Sessions.InstituteId;
            return _InstituteSubjectClassService.GetInstituteSubjectClasssByInstituteId (instituteid);
        }

        public InstituteSubjectClass Get(int id)
        {
            return _InstituteSubjectClassService.Find(id);
        }
        // GET api/Exam
        [Route("api/GetSubjectCriteria/")]
        [HttpPost]
        public List<KeyValuePair<int, string>> Get(VmCommonSearch objVmCommonSearch)
        {
             objVmCommonSearch.InstituteId = Sessions.InstituteId;
             return _InstituteSubjectClassService.GetKVP(objVmCommonSearch);
        }

      
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

        public HttpResponseMessage Post([FromBody]InstituteSubjectClass objInstituteSubjectClass)
        {
            objInstituteSubjectClass.InstituteSubject.InstituteId = Sessions.InstituteId;
            _InstituteSubjectClassService.Insert(_UnitOfWorkAsync, objInstituteSubjectClass);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/Exam/5

        public void Put(int id, [FromBody]InstituteSubjectClass objInstituteSubjectClass)
        {
            objInstituteSubjectClass.InstituteSubject.InstituteId = Sessions.InstituteId;
            _InstituteSubjectClassService.Update(_UnitOfWorkAsync, objInstituteSubjectClass);
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
