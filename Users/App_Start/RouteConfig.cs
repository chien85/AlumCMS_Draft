﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Users
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Main" },
                namespaces: new[] { "Users.Controllers" }
            );
            routes.MapRoute(
                  name: "Default2",
                  url: "Home/Pages/{idcategory}/{idsub}",
                  defaults: new { controller = "Home", action = "Pages", page = 1 }
              );

            routes.MapRoute(
                name: "Pages1",
                url: "Home/Pages/{idcategory}/{idsub}/page{page}",
                defaults: new { controller = "Home", action = "Pages" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
              name: "Default3",
              url: "Home/Pages/{idcategory}/page{page}",
              defaults: new { controller = "Home", action = "Pages", idsub = 0, page=1 }
          );

            routes.MapRoute(
            name: "Article",
            url: "Home/Content/{idcontent}/{slug}",
            defaults: new { controller = "Home", action = "Content" }
        );
        }
    }
}