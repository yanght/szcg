using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Szcg.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "main",
               url: "main.html",
               defaults: new { controller = "Home", action = "Main" }
           );

          //  routes.MapRoute(
          //    name: "Default1",
          //    url: "{controller}/{action}.html",
          //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}