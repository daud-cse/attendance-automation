
using System.Web.Mvc;
using deepp.Service.ViewModels;
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
            string url = HttpContext.Request.Url.Host;
            url = "sup.bddigitalconsultancy.com";
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

            string url = HttpContext.Request.Url.Host;
            url = "seipattendance.bddigitalconsultancy.com";
            var vmLanding = _vmLandingService.GetAllVmLanding(url);
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