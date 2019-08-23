using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using deepp.Service.SSOLogin;

namespace deepp.Api.Api.Settings
{
    public class AcademicSectionController : ApiController
    {
        private readonly IAcademicSectionService _academicSectionService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        private readonly ISSOService _SSOService;

        public AcademicSectionController(IAcademicSectionService academicSectionService
            , ISSOService SSOService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicSectionService = academicSectionService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _SSOService = SSOService;


        }
        [Route("api/AcademicSection")]
        public IEnumerable<AcademicSection> Get()
        {
            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                IEnumerable<AcademicSection> AcademicSectionList = new AcademicSection[] { new AcademicSection { Name = "Token Is not found" } };
                return AcademicSectionList;
            }
            else
            {
                return _academicSectionService.GetAcademicSections(objSSO.InstituteId);
            }
           
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

        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }
    }
}
