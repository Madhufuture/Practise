using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCRouting
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "OnlineActivation",
                url: "{controller}/{action}/{id}",
                defaults:new {area="OnlineActivation",controller="OnlineActivation",action="Index"}
                );

            routes.MapRoute(
                name: "LandingPages",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "LandingPages", controller = "LandingPages", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
