using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.Service.SSOLogin;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace sfa.Api.Api.Settings
{
    //[RoutePrefix("api/AttendanceType")]
    public class AttendanceTypeController : ApiController
    {
        private readonly IAttendanceTypeService _AttendanceTypeService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISSOService _SSOService;


        public AttendanceTypeController(IAttendanceTypeService AttendanceTypeService
            , IUnitOfWorkAsync unitOfWorkAsync
            , ISSOService SSOService)
        {
            _AttendanceTypeService = AttendanceTypeService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _SSOService = SSOService;


        }
        [Route("api/AttendanceType/new")]
        public AttendanceType GetNew()
        {
            var AttendanceTypenew = _AttendanceTypeService.NewAttendanceType();
            return new AttendanceType() { ColourList = AttendanceTypenew.ColourList };
        }
          [Route("api/attendancetype")]
        // GET api/AttendanceType
        public IEnumerable<AttendanceType> Get(bool IsActive = false)
        {



            var objSSO = _SSOService.IsTokenValid(this.Request.Headers);
            if (objSSO == null)
            {
                IEnumerable<AttendanceType> AttendanceTypeList = new AttendanceType[] { new AttendanceType { Name = "Token Is not found" } };
                return AttendanceTypeList;
            }
            else
            {
                IsActive = true;
                return _AttendanceTypeService.GetAttendanceTypes(objSSO.InstituteId, IsActive).ToList();
            }
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
