using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FSP.API
{
    public static class WebApiConfig
    {
        private const string ANY = "*";

        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            var cors = new EnableCorsAttribute(Origins, Headers, Methods);
            config.EnableCors(cors);

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static string Origins
        {
            get
            {
                string origins = ConfigurationManager.AppSettings["origins"];
                if (string.IsNullOrEmpty(origins))
                    origins = ANY;
                return origins;
            }
        }

        private static string Headers
        {
            get
            {
                string headers = ConfigurationManager.AppSettings["headers"];
                if (string.IsNullOrEmpty(headers))
                    headers = ANY;
                return headers;
            }
        }

        private static string Methods
        {
            get
            {
                string methods = ConfigurationManager.AppSettings["methods"];
                if (string.IsNullOrEmpty(methods))
                    methods = ANY;
                return methods;
            }
        }
    }
}
