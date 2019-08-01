using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace pnsms.landingTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public List<int> abc = new List<int>();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }


    #region Session

    public partial class Sessions
    {
        public static int InstituteId
        {
            get { return GetFromSession<int>("ins"); }
            set { SetInSession("ins", value); }
        }
        public static int InstituteLogoId
        {
            get { return GetFromSession<int>("Logo"); }
            set { SetInSession("Logo", value); }
        }
        public static int InstituteBannerId
        {
            get { return GetFromSession<int>("Banner"); }
            set { SetInSession("Banner", value); }
        }

        public static string InstituteContactText
        {
            get { return GetFromSession<string>("ContactText"); }
            set { SetInSession("ContactText", value); }
        }

        public static object Temp
        {
            get { return GetFromSession("temp"); }
            set { SetInSession("temp", value); }
        }

        public static string AuthenticationCode
        {
            get { return GetFromSession<string>("ac"); }
            set { SetInSession("ac", value); }
        }

        public static string MobileNumber
        {
            get { return GetFromSession<string>("mb"); }
            set { SetInSession("mb", value); }
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

    #endregion
}
