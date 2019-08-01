using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.erp;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.Api.Api.Exams
{
    public class ExamSubjectMarkController : ApiController
    {
        [Dependency]
        public IExamSubjectMarkService _examSubjectMarkService { get; set; }
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }


        // GET api/ExamSubjectMark

        public IEnumerable<ExamSubjectMark> Get(bool isActive = false)
        {
            return _examSubjectMarkService.GetExamSubjectMark(Sessions.InstituteId);
        }

        // new student only
        [Route("api/examsubjectmark/newexamsubjectmark")]
        public ExamSubjectMark GetNewExamSubjectMarkModel()
        {
            var InstituteId = Sessions.InstituteId;
            var SessionId = Sessions.CurrentSessionId;
            var objExamSubjectMark = _examSubjectMarkService.newExamSubjectMark(InstituteId, SessionId);
            return objExamSubjectMark;
        }
        public ExamSubjectMark Get(int id)
        {
            return _examSubjectMarkService.GetExamSubjectMarkById(id);
        }
        [Route("api/GetExamSubjectMarksByCriteria/")]
        [HttpPost]
        public List<ExamSubjectMark> Get(VmCommonSearch objVmCommonSearch)
        {
            objVmCommonSearch.InstituteId = Sessions.InstituteId;
            objVmCommonSearch.CurrentSessionId = Sessions.CurrentSessionId;
            return _examSubjectMarkService.GetExamSubjectMarksByCriteria(objVmCommonSearch);
        }

        // POST api/ExamSubjectMark
        [Validate]
        [Route("api/SaveExamSubjectMark/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] List<ExamSubjectMark> lstExamSubjectMark)
        {         
            _examSubjectMarkService.SaveExamSubjectMarksList(Sessions.InstituteId, UnitOfWorkAsync, lstExamSubjectMark);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/ExamSubjectMark/5
        [Validate]
        public void Put(int id, [FromBody]ExamSubjectMark ExamSubjectMark)
        {
            ExamSubjectMark.InstituteId = Sessions.InstituteId;
            _examSubjectMarkService.Update(UnitOfWorkAsync, ExamSubjectMark);
        }

        // DELETE api/ExamSubjectMark/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
