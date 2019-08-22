using deepp.erp;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api
{
    public class AcademicSessionController : ApiController
    {
      
        private readonly IAcademicSessionService _academicSessionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AcademicSessionController(IAcademicSessionService academicSessionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicSessionService = academicSessionService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/academicbranch
        public IEnumerable<AcademicSession> Get()
        {
            return _academicSessionService.GetAcademicSessions(Sessions.InstituteId);
        }

 
        // GET api/academicbranch/5
        public AcademicSession Get(int id)
        {
            return _academicSessionService.GetAcademicSessionById(id);
        }

        // POST api/academicSession
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicSession academicSession)
        {
            academicSession.InstituteId = Sessions.InstituteId;
            
            _academicSessionService.Insert(_unitOfWorkAsync,academicSession);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/academicSession/5
        [Validate]
        public void Put(int id, [FromBody]AcademicSession academicSession)
        {

            academicSession.InstituteId = Sessions.InstituteId;
            _academicSessionService.Update(_unitOfWorkAsync,academicSession);
            _unitOfWorkAsync.SaveChanges();

        }

        // DELETE api/academicSession/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
