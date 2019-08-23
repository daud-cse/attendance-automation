using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using deepp.landing.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.IdentityModel.Services;
using deepp.Service.ViewModels;
using deepp.utility;
using deepp.Service;
using Repository.Pattern.UnitOfWork;


namespace deepp.landing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVmLandingService _vmLandingService;
        private readonly IInstituteService instituteService;
        private IUnitOfWork unitOfWork;
        public HomeController(IVmLandingService vmLandingService, IInstituteService instituteService, IUnitOfWork unitOfWork)
        {
            _vmLandingService = vmLandingService;
            this.instituteService = instituteService;
            this.unitOfWork = unitOfWork;
        }
        
        public ActionResult Index()
        {
         //  string url = "demo.shikkhaforall.com";
          //  string url = "www.chaprashirhathighschool.edu.bd";            
           string url=HttpContext.Request.Url.Host;            
            var vmLanding = _vmLandingService.GetAllVmLanding(url);
            //var vmLanding = _vmLandingService.GetAllVmLanding(1);
            Sessions.InstituteId = vmLanding.Institute.Id;
            Sessions.InstituteLogoId = vmLanding.ImageLogo.Id;
            Sessions.InstituteBannerId = vmLanding.ImageBanner.Id;
            Sessions.InstituteContactText = vmLanding.Institute.ContactText;
            Sessions.InstituteUsefulLinkText = vmLanding.Institute.UsefulLinkText;

            var count = vmLanding.Institute.VisitorTotal ?? 0;

            vmLanding.Institute.VisitorTotal = ++count;
            instituteService.Update(vmLanding.Institute);
            unitOfWork.SaveChanges();

            return View(vmLanding);
        }
        public ActionResult IndexLogin()
        {
          //  string url = "demo.shikkhaforall.com";
            //  string url = "www.chaprashirhathighschool.edu.bd";            
             string url=HttpContext.Request.Url.Host;            
            var vmLanding = _vmLandingService.GetAllVmLanding(url);
            //var vmLanding = _vmLandingService.GetAllVmLanding(1);
            Sessions.InstituteId = vmLanding.Institute.Id;
            Sessions.InstituteLogoId = vmLanding.ImageLogo.Id;
            Sessions.InstituteBannerId = vmLanding.ImageBanner.Id;
            Sessions.InstituteContactText = vmLanding.Institute.ContactText;
            Sessions.InstituteUsefulLinkText = vmLanding.Institute.UsefulLinkText;

            var count = vmLanding.Institute.VisitorTotal ?? 0;

            vmLanding.Institute.VisitorTotal = ++count;
            instituteService.Update(vmLanding.Institute);
            unitOfWork.SaveChanges();

            return View(vmLanding);
        }

        public ActionResult Welcome()
        {
            return View();
        }

      
        public JsonResult keepSessionAlive()
        {
            return Json(true);
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