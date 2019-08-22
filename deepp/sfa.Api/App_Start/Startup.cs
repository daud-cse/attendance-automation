using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.ComponentModel;
using sfa.Api.Provider;
using System.Web.Mvc;
using sfa.Api;
using System.Web.Routing;
using System.Web.Optimization;
using sfa.Api.App_Start;
using Microsoft.Practices.Unity.Mvc;

[assembly: OwinStartup(typeof(sfa.Api.Startup))]

namespace sfa.Api
{
    public partial class Startup
    {
        //public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        //static Startup()
        //{
           

        //    OAuthOptions = new OAuthAuthorizationServerOptions
        //    {
        //        TokenEndpointPath = new PathString("/token"),
        //        Provider = new oAuthProvider(),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
        //        AllowInsecureHttp = true
        //    };

       

        //    //AreaRegistration.RegisterAllAreas();
        //    //GlobalConfiguration.Configure(WebApiConfig.Register);
        //    //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    //RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    //BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}       
        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    app.UseOAuthBearerTokens(OAuthOptions);
        //}
    }
}

