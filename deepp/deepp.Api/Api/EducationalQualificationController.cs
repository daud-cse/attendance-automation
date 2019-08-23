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

namespace deepp.Api.Api
{
    public class EducationalQualificationController : ApiController
    {
        private readonly IEducationalQualificationService _educationalQualificationService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public EducationalQualificationController(IEducationalQualificationService educationalQualificationService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _educationalQualificationService = educationalQualificationService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/academicbranch
        public IEnumerable<EducationalQualification> Get()
        {
            return _educationalQualificationService.GetEducationalQualifications(Sessions.InstituteId);
        }


        // GET api/academicbranch/5
        public EducationalQualification Get(int id)
        {
            return _educationalQualificationService.GetEducationalQualificationById(id);
        }

        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]EducationalQualification educationalQualification)
        {
            educationalQualification.InstituteId = Sessions.InstituteId;
            educationalQualification.LastUpdateTime = DateTime.Now;
            _educationalQualificationService.Insert(_unitOfWorkAsync, educationalQualification);
            _unitOfWorkAsync.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/department/5
        [Validate]
        public void Put(int id, [FromBody]EducationalQualification educationalQualification)
        {
            educationalQualification.InstituteId = Sessions.InstituteId;
            educationalQualification.LastUpdateTime = DateTime.Now;
            _educationalQualificationService.Update(_unitOfWorkAsync, educationalQualification);
            _unitOfWorkAsync.SaveChanges();

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
