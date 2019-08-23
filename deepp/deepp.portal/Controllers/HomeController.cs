using deepp.Entities.StoredProcedures.Models;
using deepp.Entities.ViewModels.DashBoard;
using deepp.Service;
using deepp.Service.DashBoard;
using deepp.Service.ViewModels;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deepp.portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserInfoService userInfoService;
        private readonly IRightsService rightsService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IDashboardService _dashboardService;
        private readonly IVMportalService _vMportalService;

        public HomeController(
                IUserInfoService userInfoService,
                 IRightsService rightsService,
                 //, 
                 IUnitOfWorkAsync unitOfWork
                , IVMportalService vMportalService
               , IDashboardService dashboardService
            )
        {
            this.userInfoService = userInfoService;
            this.rightsService = rightsService;
            _unitOfWork = unitOfWork;
            _vMportalService = vMportalService;
            _dashboardService = dashboardService;
        }

        [Authorize]
        public ActionResult Index()
        {
             //var user = HttpContext.User.Identity.Name;


           var user = "1222"; //Global User
          

            //temp start
          // var user = "35";
            //temp end
            if (string.IsNullOrEmpty(user))
            {
                user = Sessions.UserId.ToString();
            }

            var userId = int.Parse(user);
            //var rights = rightsService.UserRights(userId).ToList();            
            //if (rights.Count() <= 0)
            //{
            //    return RedirectToAction("UnAuthenticated");
            //}
            Sessions.UserId = userId;
            Dashboard objDashboard = new Dashboard();
            objDashboard.lstInstitute = new List<VmInstitute>();
            var userInfo = userInfoService.GetUserAndInstituteInfoByUserId(Sessions.UserId);
            //    var userInfo=  _dashboardService.GetUserAndInstituteInfoByUserId(Sessions.UserId);
            if (userInfo == null)
            {
                userInfo = new Entities.Models.UserInfo();
            }
            Sessions.UserName = userInfo.Name.ToString();
            Sessions.UserInfoTypeId = userInfo.UserInfoTypeId;
            //////Sessions.Rights = rights;
            Sessions.InstituteId = (Int32)userInfo.InstituteId;

            //  var vm = _vMportalService.GeStudentsbyGuirdians(userId);

            //Portal.shikkhaforall.com
            if (Sessions.InstituteId == 32)//Portal.shikkhaforall.com 
            {
                if (Sessions.UserInfoTypeId == 15)//Golbal User
                {
                    objDashboard = _dashboardService.GetDashboard(Sessions.UserId, Sessions.UserInfoTypeId);
                    Sessions.GlobalPlaceName = objDashboard.GlobalPlaceName;
                }
            }


            return View(objDashboard);
        }

        public JsonResult GetDashBoard()
        {

            var dashboard = _dashboardService.GetDashboard(Sessions.UserId, Sessions.UserInfoTypeId);
            dashboard.UserId = Sessions.UserId;
            return Json(new { success = true, data = dashboard },
               JsonRequestBehavior.AllowGet);

        }

        //public MvcHtmlString GeStudentsbyGuirdians()
        //{
        //    return MvcHtmlString.Create(_vMportalService.GeStudentsbyGuirdians(Sessions.UserId));
        //}
        //public MvcHtmlString GeStudentsbyId()
        //{
        //    return MvcHtmlString.Create(_vMportalService.GeStudentsbyId(Sessions.UserId));
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult UnAuthenticated(string message)
        {
            return View(message);
        }

        [Authorize]
        public JsonResult keepSessionAlive()
        {
            return Json(true);
        }
    }
}