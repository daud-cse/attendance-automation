using deepp.erp;
using deepp.Entities.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.erp.Api
{
    public class AcademicShiftController : ApiController
    {
       private readonly IAcademicShiftService _academicShiftService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AcademicShiftController(IAcademicShiftService academicShiftService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _academicShiftService = academicShiftService;
            _unitOfWorkAsync = unitOfWorkAsync;
          
            
        }
        // GET api/academicbranch
        public IEnumerable<AcademicShift> Get()
        {
            return _academicShiftService.GetAcademicShifts(Sessions.InstituteId);
        }
         
      
        public AcademicShift Get(int id)
        {
            return _academicShiftService.GetAcademicShiftById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]AcademicShift academicShift)
        {
         
            academicShift.InstituteId = Sessions.InstituteId;
            _academicShiftService.Insert(_unitOfWorkAsync, academicShift);
           
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AcademicShift academicShift)
        { 
            academicShift.InstituteId = Sessions.InstituteId;
            _academicShiftService.Update(_unitOfWorkAsync, academicShift);
           
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
