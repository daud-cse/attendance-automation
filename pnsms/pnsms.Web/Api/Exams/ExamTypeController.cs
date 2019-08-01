using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.erp;
using pnsms.Service.Exams;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace pnsms.Api.Api.Exams
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
  // [EnableCors(origins: "http://localhost", headers: "*", methods: "post,get")]
    public class ExamTypeController : ApiController
    {

        private readonly IUnitOfWorkAsync _UnitOfWorkAsync;
        private readonly IExamTypeService _ExamTypeService;


        public ExamTypeController(IUnitOfWorkAsync UnitOfWorkAsync
          , IExamTypeService ExamTypeService
         )
        {
            _UnitOfWorkAsync = UnitOfWorkAsync;
            _ExamTypeService = ExamTypeService;

        }



        // GET api/ExamType
        [Route("api/ExamType/")]
        [HttpGet]
        public IEnumerable<ExamType> Get(bool isActive = false)
        {
            var InstituteId = Sessions.InstituteId;
            return _ExamTypeService.GetExamType(InstituteId);
        }

        public ExamType Get(int id)
        {
            return _ExamTypeService.GetExamTypeById(id);
        }
        // POST api/ExamType
        [HttpPost]
        [Route("api/SaveExamType/")]
       // 
        public HttpResponseMessage Post([FromBody]ExamType objExamType)
        {
         
            objExamType.InstituteId = Sessions.InstituteId;
            _ExamTypeService.Insert(_UnitOfWorkAsync, objExamType);            
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/ExamType/5
     // [Validate]
         [Route("api/UpdateExamType/")]
        public void Put(int id, [FromBody]ExamType ExamType)
        {
            ExamType.InstituteId = Sessions.InstituteId;
            _ExamTypeService.Update(_UnitOfWorkAsync, ExamType);
        }

        // DELETE api/ExamType/5
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
