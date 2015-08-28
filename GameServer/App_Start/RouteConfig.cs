using GameServer.App_Start;
using GameServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = @"^(?!connect)\w+$" }
            );
            //them
            routes.Add(new ServiceRoute("connect",
                new WebSocketServiceHostFactory(),
                typeof(GameWebSocketService)));
            //
            //routes.MapServiceRoute
        }
    }
}
