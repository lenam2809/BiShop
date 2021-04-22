using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BiShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "gio-hang",
                url: "Cart/AddItem/{masp}/{soluong}",
                defaults: new { controller = "Cart", action = "AddItem", masp = UrlParameter.Optional, soluong = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "About",
              url: "gioi-thieu",
              defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
              namespaces: new[] { "BiShop.Controllers" }
            );
            
              routes.MapRoute(
              name: "Blog",
              url: "tin-tuc",
              defaults: new { controller = "Home", action = "Blogs", id = UrlParameter.Optional },
              namespaces: new[] { "BiShop.Controllers" }
            );

            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
              defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
              namespaces: new[] { "BiShop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
