﻿using System.Web.Http;

namespace deepp.portal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter
           .SerializerSettings
           .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // Todo: Implement global json format
            
        }
    }
}