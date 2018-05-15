using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AdManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var matches = config.Formatters
                 .Where(f => f.SupportedMediaTypes
                              .Where(m => m.MediaType.ToString() == "application/xml" ||
                                          m.MediaType.ToString() == "text/xml")
                              .Count() > 0)
                 .ToList();
            foreach (var match in matches)
                config.Formatters.Remove(match);
        }
    }
}
