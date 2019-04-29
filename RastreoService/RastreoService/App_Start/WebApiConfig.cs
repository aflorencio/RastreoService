using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RastreoService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var hola = ""; //PROBAR SI AQUI PUEDE IR RABBIT MQ
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
