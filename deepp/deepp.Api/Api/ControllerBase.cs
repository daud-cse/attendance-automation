
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hoxro.Core.Web.Controllers
{
    public class ControllerBase: ApiController
    {
       
        protected ControllerBase()
        {
           
        }
        protected async Task<string> GetUserIdFromClaim()
        {
            var principal = this.Request.GetRequestContext().Principal as System.Security.Claims.ClaimsPrincipal;
            if (principal == null) throw new Exception("userId Cannot found");
            var claims = principal.Claims.ToList();
            var userId = claims.FirstOrDefault(c => c.Type == "userId").Value;
            return userId;
        }
        protected async Task<string> GetInstituteIdFromClaim()
        {
            var principal = this.Request.GetRequestContext().Principal as System.Security.Claims.ClaimsPrincipal;
            if (principal == null) throw new Exception("instituteid Cannot found");
            var claims = principal.Claims.ToList();
            var instituteid = claims.FirstOrDefault(c => c.Type == "instituteid").Value;
            return instituteid;
        }
        protected async Task<string> GetUserNameFromClaim()
        {
            var principal = this.Request.GetRequestContext().Principal as System.Security.Claims.ClaimsPrincipal;
            if (principal == null) throw new Exception("username Cannot found");
            var claims = principal.Claims.ToList();
            var username = claims.FirstOrDefault(c => c.Type == "username").Value;
            return username;
        }

    }
}
