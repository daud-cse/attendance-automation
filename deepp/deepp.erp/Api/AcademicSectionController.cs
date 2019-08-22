using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using deepp.erp;

namespace pnsms.erp.Api
{
    public class AcademicSectionController : ApiController
    {
        private readonly IAcademicSectionService _academicSectionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AcademicSectionController(IAcademicSectionService academicSectionService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicSectionService = academicSectionService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/academicbranch
        public IEnumerable<AcademicSection> Get()
        {
            return _academicSectionService.GetAcademicSections(Sessions.InstituteId);
        }

        public AcademicSection Get(int id)
        {
            return _academicSectionService.GetAcademicSectionById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicSection academicSection)
        {
            academicSection.InstituteId = Sessions.InstituteId;
            _academicSectionService.Insert(_unitOfWorkAsync, academicSection);
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicSection academicSection)
        {
            academicSection.InstituteId = Sessions.InstituteId;
            _academicSectionService.Update(_unitOfWorkAsync, academicSection);

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
