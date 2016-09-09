using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace WebAPIAngularJSAuthentication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatters = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatters.SerializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver();

        }
    }
}
