using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitiTwo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Notice",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Notice", action = "NoticePanel", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Company",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Company", action = "Company", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "User",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Common",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Common", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Contact",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
