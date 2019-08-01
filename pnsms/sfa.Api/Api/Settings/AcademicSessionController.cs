using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.Service.SSOLogin;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace sfa.Api.Api.Settings
{
    public class AcademicSessionController : ApiController
    {
      
        private readonly IAcademicSessionService _academicSessionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        private readonly ISSOService _SSOService;


        public AcademicSessionController(IAcademicSessionService academicSessionService
            , IUnitOfWorkAsync unitOfWorkAsync
            , ISSOService SSOService)
        {
            _academicSessionService = academicSessionService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _SSOService = SSOService;
          
            
        }
        [Route("api/AcademicSession")]
        public IEnumerable<AcademicSession> Get()
        {
            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                IEnumerable<AcademicSession> AcademicSessionList = new AcademicSession[] { new AcademicSession { Name = "Token Is not found" } };
                return AcademicSessionList;
            }
            else
            {
                return _academicSessionService.GetAcademicSessions(objSSO.InstituteId);
            }
           
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
