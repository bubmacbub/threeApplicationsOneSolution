using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MobileNewsAdminService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //This config corresponds to the nuget package Microsoft.AspNet.WebApi.Cors
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                 routeTemplate: "admin/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
