using pnsms.Entities.Models;
using pnsms.Entities.ViewModels;
using pnsms.Service;
using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pnsms.portal.Controllers
{
    public class UserInfoSecuritiesController : Controller
    {
        private readonly IUserInfoSecurityService _userInfoSecurityService;

        private readonly IUnitOfWork _unitOfWork;
        public UserInfoSecuritiesController(IUnitOfWork unitOfWork, IUserInfoSecurityService userInfoSecurityService)
        {
            _userInfoSecurityService = userInfoSecurityService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult ChangePassword(string Mgs)
        {
            VmUserInfoSecurity vmUserInfoSecurity = new VmUserInfoSecurity();
            vmUserInfoSecurity.userInfoSecurity = new UserInfoSecurity();
            if (!string.IsNullOrEmpty(Mgs))
            {
                ModelState.AddModelError("", Mgs);
            }
            return View(vmUserInfoSecurity);
        }
        public ActionResult SaveChangePassword(VmUserInfoSecurity vmUserInfoSecurity, string NewPassword, string ConfirmPassword)
        {

            string Mgs = string.Empty;
            if (ModelState.IsValid)
            {
                var user = _userInfoSecurityService.GetByUserLoginId(Sessions.UserId, Sessions.InstituteId);

                if (user == null)
                {
                 Mgs= "Invalid username or password.";

                }
                else if (NewPassword != ConfirmPassword)
                {
                    Mgs= "NewPassword and ConfirmPassword are not same.";

                }
                else if (user.PasswordHash == EncryptionDecreption.EncryptToMD5(vmUserInfoSecurity.userInfoSecurity.PasswordHash))
                {
                    _userInfoSecurityService.UpdatePassword(_unitOfWork, Sessions.UserId, NewPassword);
                   Mgs="Password Change Successfully.";
                }
                else
                {
                  Mgs=  "Invalid username or password.";

                }
            }           
            return RedirectToAction("ChangePassword", new { Mgs = Mgs });
        }
    }
}