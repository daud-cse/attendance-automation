using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
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
    public class SubjectController : ApiController
    {
        //Student 

        [Dependency]
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }
        [Dependency]
        public ISubjectService SubjectService { get; set; }

        public IEnumerable<Subject> Get(bool isActive = false)
        {
            var result = SubjectService.Get( null);
            var kresult =
                result.Select(
                    s => new Subject() { Id = s.Id, Name = s.Name, ShortName = s.ShortName, OrderBy = s.OrderBy, Code = s.Code, Description = s.Description, ParentSubjectId = s.ParentSubjectId, IsActive = s.IsActive, TagId = s.TagId });
            return kresult;
        }

        public Subject Get(int id)
        {
            return SubjectService.Get(id);
        }

        // POST api/subject
        [Validate]
        public HttpResponseMessage Post([FromBody]Subject subject)
        {
           // subject.InstituteId = Sessions.InstituteId;
            SubjectService.Insert(UnitOfWorkAsync, subject);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/subject/5
        [Validate]
        public void Put(int id, [FromBody]Subject subject)
        {
            //subject.InstituteId = Sessions.InstituteId;
            SubjectService.Update(UnitOfWorkAsync, subject);
        }

        // DELETE api/Subject/5
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
