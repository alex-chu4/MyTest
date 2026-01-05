using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MSP.API
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

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "msp/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static string Origins
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["origins"]))
                    return ConfigurationManager.AppSettings["origins"];
                else
                    return ANY;
            }
        }

        private static string Headers
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["headers"]))
                    return ConfigurationManager.AppSettings["headers"];
                else
                    return ANY;
            }
        }

        private static string Methods
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["methods"]))
                    return ConfigurationManager.AppSettings["methods"];
                else
                    return ANY;
            }
        }
    }
}
