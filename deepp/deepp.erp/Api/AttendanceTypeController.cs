using deepp.erp;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace pnsms.erp.Api
{
    //[RoutePrefix("api/AttendanceType")]
    public class AttendanceTypeController : ApiController
    {
        private readonly IAttendanceTypeService _AttendanceTypeService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public AttendanceTypeController(IAttendanceTypeService AttendanceTypeService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _AttendanceTypeService = AttendanceTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        [Route("api/AttendanceType/new")]
        public AttendanceType GetNew()
        {
            var AttendanceTypenew = _AttendanceTypeService.NewAttendanceType();
            return new AttendanceType() { ColourList = AttendanceTypenew.ColourList };
        }

        // GET api/AttendanceType
        public IEnumerable<AttendanceType> Get(bool IsActive = false)
        {
            List<AttendanceType> AttendanceTypeFinal = new List<AttendanceType>();
            var AttendanceType = _AttendanceTypeService.GetAttendanceTypes(Sessions.InstituteId,IsActive).ToList();
            return AttendanceType;
        }

        // [Route ]
        //[Route("New")]
        // [Route("New/{id:int}")]
        // [HttpPost]
        public AttendanceType Put()
        {
            var AttendanceType = _AttendanceTypeService.NewAttendanceType();
            return AttendanceType;
        }
       
        // GET api/AttendanceType/5
        public AttendanceType Get(int id)
        {
            return _AttendanceTypeService.GetAttendanceTypeById(id);
        }

        // POST api/AttendanceType
        // [Validate]
        public HttpResponseMessage Post([FromBody]AttendanceType AttendanceType)
        {

            AttendanceType.InstituteId = Sessions.InstituteId;
            AttendanceType.LastUpdateTime = DateTime.Now;
           _AttendanceTypeService.Insert(AttendanceType);
            _unitOfWorkAsync.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]AttendanceType AttendanceType)
        {
            AttendanceType.InstituteId = Sessions.InstituteId;
            _AttendanceTypeService.Update(AttendanceType);
            _unitOfWorkAsync.SaveChanges();

        }

        // DELETE api/AttendanceType/5
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
