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
    public class AcademicBranchController : ApiController
    {


        private readonly IAcademicBranchService _academicBranchService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        private readonly ISSOService _SSOService;

        public AcademicBranchController(IAcademicBranchService academicBranchService
            , ISSOService SSOService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicBranchService = academicBranchService;
            _SSOService = SSOService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/academicbranch
          [Route("api/AcademicBranch/")]
        public IEnumerable<AcademicBranch> Get()
        {
            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
          {
              IEnumerable<AcademicBranch> AcademicBranchlst = new AcademicBranch[] { new AcademicBranch { Name = "Token Is not found" } };
              return AcademicBranchlst;
          }
          else
          {
              return _academicBranchService.GetAcademicBranchs(objSSO.InstituteId);
          }
          
        }


        
        public AcademicBranch Get(int id)
        {
            return _academicBranchService.GetAcademicBranchById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicBranch academicBranch)
        {
            academicBranch.InstituteId = Sessions.InstituteId;
            _academicBranchService.Insert(_unitOfWorkAsync,academicBranch);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicBranch academicBranch)
        {
            academicBranch.InstituteId = Sessions.InstituteId;
            _academicBranchService.Update(_unitOfWorkAsync,academicBranch);
           

        }

        // DELETE api/academicbranch/5
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
