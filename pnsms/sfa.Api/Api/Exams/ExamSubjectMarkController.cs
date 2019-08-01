using Microsoft.Practices.Unity;
using pnsms.Entities.Models;

using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace sfa.Api.Api.Exams
{
    public class ExamSubjectMarkController : ApiController
    {
        [Dependency]
        public IExamSubjectMarkService ExamSubjectMarkService { get; set; }
        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }


        // GET api/ExamSubjectMark

        public IEnumerable<ExamSubjectMark> Get(bool isActive = false)
        {
            return ExamSubjectMarkService.GetExamSubjectMark(Sessions.InstituteId);
        }

        public ExamSubjectMark Get(int id)
        {
            return ExamSubjectMarkService.GetExamSubjectMarkById(id);
        }
        // POST api/ExamSubjectMark
        [Validate]
        public HttpResponseMessage Post([FromBody]ExamSubjectMark ExamSubjectMark)
        {
            ExamSubjectMark.InstituteId = Sessions.InstituteId;
            ExamSubjectMarkService.Insert(UnitOfWorkAsync, ExamSubjectMark);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/ExamSubjectMark/5
        [Validate]
        public void Put(int id, [FromBody]ExamSubjectMark ExamSubjectMark)
        {
            ExamSubjectMark.InstituteId = Sessions.InstituteId;
            ExamSubjectMarkService.Update(UnitOfWorkAsync, ExamSubjectMark);
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
