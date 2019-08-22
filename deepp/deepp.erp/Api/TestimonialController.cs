using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using deepp.erp;

namespace pnsms.erp.Api
{
    public class TestimonialController : ApiController
    {
         private readonly ITestimonialService _iTestimonialService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;



        public TestimonialController(ITestimonialService iTestimonialService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _iTestimonialService = iTestimonialService;
            _unitOfWorkAsync = unitOfWorkAsync;


        }
        // GET api/testimonial
        public IEnumerable<Testimonial> Get()
        {
            return _iTestimonialService.GetTestimonialByInstituteId(Sessions.InstituteId);
        }

        // GET api/testimonial/5
        public Testimonial Get(int id)
        {
            return _iTestimonialService.GetTestimonialById(id);
        }

        // POST api/testimonial
        public void Post([FromBody]Testimonial testimonial)
        {
            testimonial.InstituteId = Sessions.InstituteId;
            testimonial.LastUpdateTime = DateTime.Now;
            _iTestimonialService.CreateTestimonial(_unitOfWorkAsync, testimonial);
        }

        // PUT api/testimonial/5
        public void Put(int id, [FromBody]Testimonial testimonial)
        {
            testimonial.InstituteId = Sessions.InstituteId;
            testimonial.LastUpdateTime = DateTime.Now;
            _iTestimonialService.UpdateTestimonial(_unitOfWorkAsync, testimonial);
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
