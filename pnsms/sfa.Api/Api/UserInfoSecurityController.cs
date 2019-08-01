using pnsms.Entities.Models;
using pnsms.Service;
using pnsms.utility;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sfa.Api.Api
{
    public class UserInfoSecurityController : ApiController
    {
        private readonly IUserInfoSecurityService _userSecurityService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly int userId = Sessions.UserId;

        public UserInfoSecurityController(
              IUserInfoSecurityService userSecurityService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _userSecurityService = userSecurityService;
            _unitOfWorkAsync = unitOfWorkAsync;

        }

        [Route("api/usersecurity/passwordchange")]
        [HttpPost]
        public HttpResponseMessage PasswordChange([FromBody]UserInfoSecurity userSecurityModel)
        {            
            var UserSecInfo = _userSecurityService.GetByUserId(userId);
            var ss = new HttpResponseMessage();
            
            if (UserSecInfo != null && UserSecInfo.PasswordHash == EncryptionDecreption.EncryptToMD5(userSecurityModel.PasswordHash))
            {
                if (userSecurityModel.NewPassword == userSecurityModel.ConfirmPassword)
                {
                    _userSecurityService.UpdatePassword(_unitOfWorkAsync, Sessions.UserId, userSecurityModel.NewPassword);

                     ss.StatusCode = HttpStatusCode.Created;
                }
                else {
                    ss.ReasonPhrase = "New Passowrd & Confirm Password Not Matched!";
                    ss.StatusCode = HttpStatusCode.Conflict; }

            }
            else {
                ss.ReasonPhrase = "Wrong Old Password Submitted!";
                ss.StatusCode = HttpStatusCode.Conflict;
            
            }
            return ss;
            
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
