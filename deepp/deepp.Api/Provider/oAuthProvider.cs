using deepp.Api;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using deepp.Service;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using deepp.utility;
using Repository.Pattern.UnitOfWork;
using deepp.Entities.Models;

namespace deepp.Api.Provider
{
    public class oAuthProvider:OAuthAuthorizationServerProvider
    {
       
         IStoredProcedures _storedProcedures;
        
        //public oAuthProvider(
        //    , IStoredProcedures storedProcedures
        // )
        //{
        //    _unitOfWork = unitOfWork;
        //    _userInfoSecurityService = userInfoSecurityService;
        //    _rightsService = rightsService;
        //    _instituteService = instituteService;
        //    _storedProcedures = storedProcedures;
        //}

        //public oAuthProvider()
        //{
            
        //}
        //public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        var username = context.UserName;
        //        var password = context.Password;

        //        var encriptpass = EncryptionDecreption.EncryptToMD5(password);

        //        _storedProcedures = new PNSMSContext();

        //        var user = _storedProcedures.GetUserDetails(5, username, encriptpass);

        //        //var user = _userInfoSecurityService.GetByUserLoginName(username, 5);
        //        if (user != null)
        //        {
        //            var claims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.Name, username),
        //                new Claim("userid", user.objUserInfoDetails.UserInfoId.ToString()),
        //                new Claim("instituteid", user.objUserInfoDetails.InstituteId.ToString()),
        //                new Claim("username", user.objUserInfoDetails.UserName.ToString()),
        //            };

        //            ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
        //            context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
        //        }
        //        else
        //        {
        //            context.SetError("invalid_grant", "Error");
        //        }
        //    });
        //}

        //public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    if (context.ClientId == null)
        //    {
        //        context.Validated();
        //    }
        //    return Task.FromResult<object>(null);
        //}
    }
}