using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAPIAngularJSAuthentication.Startup))]
namespace WebAPIAngularJSAuthentication
{
    public class Startup
    {
        // app parameter is used to compose the application for owin server.
        public void Configuration(IAppBuilder app)
        {
            //config is used to configure the api routes.
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            //UseWebApi is responsible to wire up WebAPI to Owin Server Pipeline.
            app.UseWebApi(config);
        }
    }
}