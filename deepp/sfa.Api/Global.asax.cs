using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace sfa.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                Response.Flush();
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
          //  WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
     
    }


    public partial class Sessions
    {
        public static int UserId
        {
            get { return GetFromSession<int>("id"); }
            set { SetInSession("id", value); }
        }
        public static List<string> Rights
        {
            get { return GetFromSession<List<string>>("rights"); }
            set { SetInSession("rights", value); }
        }
        public static int InstituteId
        {
            get { return GetFromSession<int>("ins"); }
            set { SetInSession("ins", value); }
        }

        public static object Temp
        {
            get { return GetFromSession("temp"); }
            set { SetInSession("temp", value); }
        }

        static object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        static T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }
        static void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}
