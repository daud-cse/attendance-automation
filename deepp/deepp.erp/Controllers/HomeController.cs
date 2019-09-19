using deepp.erp.Models;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Linq;
using System.Web.Mvc;


namespace deepp.erp.Controllers
{
    public class HomeController : Controller
    {
        readonly IUserInfoService userInfoService;
        readonly IRightsService rightsService;
        readonly IUnitOfWorkAsync uow;

        public HomeController(IUserInfoService userInfoService,
            IRightsService rightsService,
            IUnitOfWorkAsync uow)
        {
            this.userInfoService = userInfoService;
            this.rightsService = rightsService;
            this.uow = uow;
        }

        [deepp.erp.Authorize]
        public ActionResult Index()
        {
            string user = HttpContext.User.Identity.Name;
            //temp end
            user = "42";//seip    
                        // user = "82";//ionex
            try
            {
                if (string.IsNullOrEmpty(user))
                {
                    user = Sessions.UserId.ToString();
                }
                int userId = int.Parse(user);

                var rights = rightsService.UserRights(userId).ToList();

                if (rights.Count() <= 0)
                {
                    return RedirectToAction("UnAuthenticated");
                }
                Sessions.Rights = rights;
                Sessions.UserId = userId;
            }
            catch (Exception ex)
            {
                return RedirectToAction("SessionsOut");
            }
            return View();
        }

        [deepp.erp.Authorize]
        public ActionResult Dashboard()
        {

            var user = userInfoService.GetUserAndInstituteInfoByUserId(Sessions.UserId);
            Sessions.InstituteId = user.InstituteId.Value;
            LandingViewModel model = new LandingViewModel();
            model.InstituteName = user.Institute.Name;
            model.UserName = user.Name;
            var objAcademicSessions = user.Institute.AcademicSessions.AsQueryable().Where(x => x.IsRunning == true).FirstOrDefault();
            Sessions.CurrentSessionId = objAcademicSessions.Id;
            model.CurrentSessionName = objAcademicSessions.Name;
            return PartialView(model);
        }
        [deepp.erp.Authorize]
        public ActionResult Navigation()
        {
            return PartialView();
        }
        [deepp.erp.Authorize]
        public ActionResult Header()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult UnAuthenticated(string message)
        {
            return View(message);
        }
        public ActionResult SessionsOut(string message)
        {
            return View(message);
        }
        [deepp.erp.Authorize]
        public JsonResult keepSessionAlive()
        {
            return Json(true);
        }
    }
}