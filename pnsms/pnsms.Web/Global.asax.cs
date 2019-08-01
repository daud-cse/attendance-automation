using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace pnsms.erp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        // Web-api session behavior
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }


    #region Authentication & Authorization

    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public string Rights { get; set; }

        public AuthorizeAttribute()
        {
            Rights = "";
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new RedirectToRouteResult(
            //                new RouteValueDictionary
            //                            {
            //                                    { "client", filterContext.RouteData.Values[ "client" ] },
            //                                    { "controller", "Home" },
            //                                    { "action", "UnAuthenticated" },
            //                                    {"area",""}
            //                            });
            //    return;
            //}

            //if (Rights != "")
            //{
            //    if (Rights.HasRights(Sessions.Rights))
            //    {
            //        filterContext.Result = new RedirectToRouteResult(
            //                        new RouteValueDictionary
            //                       {
            //                               { "client", filterContext.RouteData.Values[ "client" ] },
            //                               { "controller", "Home" },
            //                               { "action", "UnAuthenticated" },
            //                               {"area",""}                                           
            //                       });
            //    }

            //}
            
        }
    }

    #endregion

    #region Session

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
        public static int CurrentSessionId
        {
            get { return GetFromSession<int>("SessionId"); }
            set { SetInSession("SessionId", value); }
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
            //key = "29";
            return (T)HttpContext.Current.Session[key];
        }
        static void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }

    #endregion

    public static class RoleHelper
    {
        public static bool HasRights(this string commaSaperatedRights)
        {            
            List<string> rights = commaSaperatedRights.Split(',').ToList();

            return rights.Intersect(Sessions.Rights).Any();

        }

        public static bool HasRights(this string commaSaperatedRights, List<string> allowedRights)
        {
            List<string> rights = commaSaperatedRights.Split(',').ToList();

            return rights.Intersect(allowedRights).Any();

        }
    }
}
