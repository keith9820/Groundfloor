using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ReverseGeocode",
                url: "Location/ReverseGeocode/{latitude}/{longitude}"
            );
            routes.MapRoute(
                name: "GeocodeCity",
                url: "Location/GeocodeCity/{city}/{state}"
            );
            routes.MapRoute(
                name: "GeocodeZip",
                url: "Location/GeocodeZip/{zipcode}"
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}