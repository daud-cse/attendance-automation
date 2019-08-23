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
using deepp.Service;
using deepp.utility;
using Repository.Pattern.UnitOfWork;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using deepp.Service.ViewModels;
using deepp.Entities.Models;

namespace deepp.landing.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserInfoService userInfoService;
        readonly IUserInfoSecurityService userInfoSecurityService;
        readonly IGuardiansOfStudentService guardiansOfStudents;
        readonly IShortMessageService smsService;
        readonly IUnitOfWork unitOfWork;
        readonly IUnitOfWorkAsync unitOfWorkAsync;

        public AccountController(IUserInfoService userInfoService
            , IUserInfoSecurityService userInfoSecurityService
            , IGuardiansOfStudentService guardiansOfStudents
            , IShortMessageService smsService
            , IUnitOfWork unitOfWork
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            this.userInfoService = userInfoService;
            this.userInfoSecurityService = userInfoSecurityService;
            this.guardiansOfStudents = guardiansOfStudents;
            this.smsService = smsService;
            this.unitOfWork = unitOfWork;
            this.unitOfWorkAsync = unitOfWorkAsync;
        }


        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userInfoSecurityService.GetByUserLoginName(model.UserName, Sessions.InstituteId);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                else if (user.PasswordHash == EncryptionDecreption.EncryptToMD5(model.Password))
                {
                    SetAuthToken(user.UserInfoId);

                    string rtUrl = RedirectUrl(user.UserInfoId);
                    if (rtUrl != "")
                    {
                        return Redirect(rtUrl);
                    }

                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return RedirectToAction("IndexLogin", "Home");
        }


        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("IndexLogin", "Home");
        }

        public ActionResult RegisterMobileNo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterMobileNo(RegisterMobileNoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string mobileNo = MobileNumber.Rectify(model.MobileNo);
                Sessions.MobileNumber = mobileNo;
                //get userInfo id 

                var userInfoes = userInfoService.GetByMobileNumber(mobileNo, Sessions.InstituteId);

                if (userInfoes.Count() <= 0)
                {
                    ModelState.AddModelError("", "Mobile number is not given at office");
                    return View(model);
                }

                userInfoes = userInfoes.Where(c => c.UserInfoSecurity == null);

                if (userInfoes.Count() <= 0)
                {
                    ModelState.AddModelError("", "You have already registered");
                    return View(model);
                }

                string authCode = AuthCode(mobileNo);

                if (authCode=="")
                {
                    ModelState.AddModelError("", "Error to send. Please try again");
                    return View(model);
                }

                List<int> userInfoIds = new List<int>();

                foreach (var row in userInfoes)
                {
                    userInfoIds.Add(row.Id);
                }

                Sessions.Temp = userInfoIds;                
                Sessions.AuthenticationCode = authCode;

                return View("RegisterActivationCode");
            }

            ModelState.AddModelError("", "Invalid mobile number.");

            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterActivationCode(RegisterActivationCodeViewModel model)
        {
            //check code
            if (Sessions.AuthenticationCode != model.AuthenticationCode)
            {
                ModelState.AddModelError("", "Invalid code");
                return View(model);
            }

            RegisterViewModel mod = new RegisterViewModel();
            mod.UserRegisterInfoOnMobileNo = GetList();

            return View("Register", mod);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            Sessions.AuthenticationCode = null;
            if (ModelState.IsValid)
            {

                ErrorCode error = userInfoSecurityService.Insert(unitOfWork
                    , model.UserName
                    , model.Password
                    , Sessions.InstituteId
                    , model.UserInfoId);

                if (error != ErrorCode.NoError)
                {
                    ModelState.AddModelError("", "User name already used. Please change it and try again");
                    model.UserRegisterInfoOnMobileNo = GetList();
                    return View(model);
                }

                SetAuthToken(model.UserInfoId);

                string returnUrl = RedirectUrl(model.UserInfoId);
                if (returnUrl != "")
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Welcome", "Home");

            }


            return View(model);
        }


        public ActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPass(RegisterMobileNoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string mobileNo = MobileNumber.Rectify(model.MobileNo);
                Sessions.MobileNumber = mobileNo;
                //get userInfo id 

                var userInfoes = userInfoService.GetByMobileNumber(mobileNo,Sessions.InstituteId);

                if (userInfoes.Count() <= 0)
                {
                    ModelState.AddModelError("", "Mobile number is invalid");
                    return View(model);
                }

                userInfoes = userInfoes.Where(c => c.UserInfoSecurity != null);

                if (userInfoes.Count() <= 0)
                {
                    ModelState.AddModelError("", "You are not registered");
                    return View(model);
                }

                string authCode = AuthCode(mobileNo);

                if (authCode == "")
                {
                    ModelState.AddModelError("", "Error to send. Please try again");
                    return View(model);
                }

                List<int> userInfoIds = new List<int>();

                foreach (var row in userInfoes)
                {
                    userInfoIds.Add(row.Id);
                }

                Sessions.Temp = userInfoIds;
                Sessions.AuthenticationCode = authCode;

                return View("ForgotPassActivationCode");
            }

            ModelState.AddModelError("", "Invalid mobile number.");

            return View(model);
        }

        [HttpPost]
        public ActionResult ForgotPassActivationCode(RegisterActivationCodeViewModel model)
        {
            //check code
            if (Sessions.AuthenticationCode != model.AuthenticationCode)
            {
                ModelState.AddModelError("", "Invalid code");
                return View(model);
            }


            ForgotPassViewModel mod = new ForgotPassViewModel();
            mod.UserRegisterInfoOnMobileNo = GetList();

            return View("ForgotPassReset", mod);
        }

        [HttpPost]
        public ActionResult ForgotPassReset(ForgotPassViewModel model)
        {
            Sessions.AuthenticationCode = null;
            if (ModelState.IsValid)
            {

                ErrorCode error = userInfoSecurityService.UpdatePassword(unitOfWork
                    , model.UserInfoId
                    , model.Password);

                if (error != ErrorCode.NoError)
                {
                    ModelState.AddModelError("", "Password cannot changed. Please try again");
                    return View(model);
                }

                SetAuthToken(model.UserInfoId);

                string returnUrl = RedirectUrl(model.UserInfoId);
                if (returnUrl != "")
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Welcome", "Home");

            }


            return View(model);
        }

        public JsonResult RequestForCode()
        {
            string msg = "";
            string mobileNo = "" + Sessions.MobileNumber;

            if (mobileNo == "")
            {
                msg = "invalid mobile number";
            }
            else
            {
                string code = AuthCode(mobileNo);
                Sessions.AuthenticationCode = code;
                msg = code == "" ? "Error to send. Please try again" : "code has sent to the mobile number";
                //max 5 code for a day at every mobile number
            }

            return Json(new { msg = msg }, JsonRequestBehavior.AllowGet);
        }

        string AuthCode(string mobileNo)
        {
            string code = SecurityCode.RandomNumber(4);

            VmShortMessage vm = new VmShortMessage();

            vm.ShortMessages = new Entities.Models.ShortMessage();
            vm.ShortMessageDetails = new List<Entities.Models.ShortMessageDetail>();


            vm.ShortMessages.InstituteId = Sessions.InstituteId;
            vm.ShortMessages.SmsTemplate = "Authentication Code : " + code;
            vm.ShortMessages.IsStaticSms = true;
            vm.ShortMessages.SendAt = DateTime.Now.Date;
            vm.ShortMessages.TotalSmsCount = 1;
            vm.ShortMessages.IsSent = false;
            vm.ShortMessages.IsGenerated = true;

            vm.ShortMessageDetails.Add(new ShortMessageDetail
            {
                SmsText = "Authentication Code : " + code
                ,
                MobileNumber = mobileNo
                ,
                SmsCount = 1
                ,
                IsSent = false
            });

            smsService.Insert(unitOfWorkAsync, vm);

            try
            {
                string baseUrl = "https://powersms.banglaphone.net.bd";
                string userId = "shikkhaforall";
                string password = "Shikkhaforall123";

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.ExpectContinue = false; 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new FormUrlEncodedContent(new[] 
                    {
                    new KeyValuePair<string, string>("userId", userId)
                    ,new KeyValuePair<string, string>("password", password)
                    ,new KeyValuePair<string, string>("smsText","Authentication Code : "+ code)
                    ,new KeyValuePair<string, string>("commaSeperatedReceiverNumbers", mobileNo)                    
                    });

                    var response = client.PostAsync("/httpapi/sendsms", content).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                }
            }
            catch
            {
                return "";
            }

            return code;
        }

        List<UserRegisterInfo> GetList()
        {
            var userRegInfo = new List<UserRegisterInfo>();
            List<int> ids = (List<int>)Sessions.Temp;

            foreach (var id in ids)
            {
                UserRegisterInfo urInfo = new UserRegisterInfo();

                var grdSt = guardiansOfStudents.GetStudentsByGuardian(id);

                if (grdSt.Count() > 0)
                {
                    var singleUser = grdSt.FirstOrDefault();
                    urInfo.Name = singleUser.Guardian.UserInfo.Name;
                    urInfo.UserInfoId = singleUser.GuardianId;
                    urInfo.Students = new List<string>();

                    foreach (var st in grdSt)
                    {
                        urInfo.Students.Add(st.Student.UserInfo.Name);
                    }
                }
                else
                {
                    var user = userInfoService.GetUserById(id);
                    urInfo.Name = user.Name;
                    urInfo.UserInfoId = user.Id;
                }

                userRegInfo.Add(urInfo);

            }

            return userRegInfo;
        }

        void SetAuthToken(int userInfoId)
        {
            string url = HttpContext.Request.Url.Host;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1
                , userInfoId.ToString()
                , DateTime.Now
                , DateTime.Now.AddMinutes(5)
                , false
                , ""
                , url);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName
                , FormsAuthentication.Encrypt(ticket));

            Response.Cookies.Add(cookie);
        }

        string RedirectUrl(int userInfoId)
        {
            var userInfo = userInfoService.GetUserById(userInfoId);

            if (userInfo.UserInfoTypeId == (int)deepp.utility.UserInfoType.Employee)
            {
                return "~/erp";
            }
            else if (userInfo.UserInfoTypeId == (int)deepp.utility.UserInfoType.Teacher)
            {
                return "~/erp";
            }
            else if (userInfo.UserInfoTypeId == (int)deepp.utility.UserInfoType.Student)
            {
                return "~/portal";
            }
            else if (userInfo.UserInfoTypeId == (int)deepp.utility.UserInfoType.Guardian)
            {
                return "~/portal";
            }
            else if (userInfo.UserInfoTypeId == (int)deepp.utility.UserInfoType.Global_Users)
            {
                return "~/portal";
            }
            else if (userInfo.UserInfoTypeId == (int)deepp.utility.UserInfoType.Governingbody)
            {
                return "~/portal";
            }
            return "";
        }
    }
}