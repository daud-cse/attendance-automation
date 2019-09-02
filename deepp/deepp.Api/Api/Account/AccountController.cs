
using deepp.Entities.Models;
using deepp.Service;
using deepp.Service.SSOLogin;
using deepp.utility;
using Repository.Pattern.UnitOfWork;
using deepp.Api.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace deepp.Api.Account
{
    public class AccountController : ApiController
    {


        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IInstituteService _instituteService;
        private readonly IStoredProcedures _storedProcedures;
        private readonly IUserInfoSecurityService _userInfoSecurityService;
        private readonly IRightsService _rightsService;
        private readonly ISSOService _SSOService;
        public AccountController(IUnitOfWorkAsync unitOfWork
            , IInstituteService instituteService
            , IUserInfoSecurityService userInfoSecurityService
            , IRightsService rightsService
            , IStoredProcedures storedProcedures
            , ISSOService SSOService
         )
        {
            _unitOfWork = unitOfWork;
            _userInfoSecurityService = userInfoSecurityService;
            _rightsService = rightsService;
            _instituteService = instituteService;
            _storedProcedures = storedProcedures;
            _SSOService = SSOService;
        }


        [Route("api/Login/")]
        [HttpPost]
        //  [EnableCors(origins: "http://api.shikkhaforall.com", headers: "*", methods: "*")]
        public HttpResponseMessage Login([FromBody]LoginRequestModel objLoginRequestModel)
        {
            SessionIDManager manager = new SessionIDManager();
            string sessionId = manager.GetSessionID(HttpContext.Current);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var UserDetails = _storedProcedures.GetUserDetails(objLoginRequestModel.instituteid, objLoginRequestModel.userid, EncryptionDecreption.EncryptToMD5(objLoginRequestModel.password));
            LoginReponseModel objLoginReponseModel = new LoginReponseModel();
            try { 
            if (UserDetails.objUserInfoDetails != null)
            {
                SSO objSSo = new SSO();
                objSSo.InstituteId = objLoginRequestModel.instituteid;
                objSSo.AcademicSessionId=UserDetails.objUserInfoDetails.AcademicSessionId;
                objSSo.Tokenkey = Guid.NewGuid().ToString();
                objSSo.UserId=UserDetails.objUserInfoDetails.UserInfoId;
                objSSo.UserName = UserDetails.objUserInfoDetails.UserName;
                objSSo.SessionId = sessionId;
                objSSo.LogDate = DateTime.Now;
                objSSo.IssuedUtc = DateTime.Now;
                objSSo.ExpiresUtc = DateTime.Now;
                _SSOService.Insert(_unitOfWork, objSSo);

                objLoginReponseModel.Success = "1";
                objLoginReponseModel.Message = "Login Successfully";
                objLoginReponseModel.Token = objSSo.Tokenkey;
                objLoginReponseModel.Obj = UserDetails;
                response = Request.CreateResponse(HttpStatusCode.OK, objLoginReponseModel);
                response.Headers.Add("token", objSSo.Tokenkey);
                return response;
            }
            else
            {
                objLoginReponseModel.Success = "2";
                objLoginReponseModel.Message = "Invalid Login";
                objLoginReponseModel.Token = null;
                objLoginReponseModel.Obj = UserDetails;
                return response;
            }
            }
            catch (Exception ex)
            {
                return response;
            }
        }



    }
}
