using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IntegradorAtendimentosRDStation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "RouteApi",
                routeTemplate: "api/{controller}/{route}",
                defaults: new { route = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SendLeadRouteApi",
                routeTemplate: "api/sendlead/{A261_CD_CONT}",
                defaults: new { A261_CD_CONT = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SendConversionRouteApi",
                routeTemplate: "api/sendconversion/{A022_CD_EV}/{A012_CD_CLI}/{A261_CD_CONT}"
            );
        }
    }
}
