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
    public class GuardianTypeController : ApiController
    {
        private readonly IGuardianTypeService _guardianTypeService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GuardianTypeController(IGuardianTypeService guardianTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _guardianTypeService = guardianTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/academicbranch
        public IEnumerable<GuardianType> Get(bool IsActive = false)
        {
            return _guardianTypeService.GetGuardianTypes(Sessions.InstituteId);
        }
        // GET api/academicbranch/5
        public GuardianType Get(int id)
        {
            return _guardianTypeService.GetGuardianTypeById(id);
        }

        public HttpResponseMessage Post([FromBody]GuardianType guardianType)
        {

            guardianType.InstituteId = Sessions.InstituteId;
            _guardianTypeService.Insert(_unitOfWorkAsync, guardianType);

            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        public void Put(int id, [FromBody]GuardianType guardianType)
        {
            guardianType.InstituteId = Sessions.InstituteId;
            _guardianTypeService.Update(_unitOfWorkAsync, guardianType);

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
