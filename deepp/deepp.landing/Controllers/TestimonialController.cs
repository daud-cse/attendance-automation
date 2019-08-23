using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.landing.Controllers
{
    public class TestimonialController : Controller
    {
        
        private readonly ITestimonialService _testimonialService;
        private readonly IUnitOfWork _unitOfWork;
        public TestimonialController(IUnitOfWork unitOfWork, ITestimonialService testimonialService)
        {
        
            _testimonialService = testimonialService;        
            _unitOfWork = unitOfWork;
        }
       /// <summary>
       /// Get Active Testimonial
       /// </summary>
       /// <returns></returns>
        public ActionResult GetActiveTestimonial()
        {
            var InstituteId = Sessions.InstituteId;
            var testimonial = _testimonialService.GetActiveTestimonialByInstituteId(InstituteId);
            return PartialView("GetPartialTestimonial", testimonial);

        }
        //protected override void Dispose(bool disdeepping)
        //{
        //    if (disdeepping)
        //    {
        //        _unitOfWork.Dispose();
        //    }
        //    base.Dispose(disdeepping);
        //}
    }
}
