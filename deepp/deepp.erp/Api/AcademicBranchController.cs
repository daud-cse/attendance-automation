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
    public class AcademicBranchController : ApiController
    {


        private readonly IAcademicBranchService _academicBranchService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AcademicBranchController(IAcademicBranchService academicBranchService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicBranchService = academicBranchService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/academicbranch
        public IEnumerable<AcademicBranch> Get()
        {
            return _academicBranchService.GetAcademicBranchs(Sessions.InstituteId);
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
