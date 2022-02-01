using System.Web.Mvc;
using System.Web.Routing;

namespace Sys.Produccion
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "Sys.Produccion.Controllers" }
        );
        }
    }
}
