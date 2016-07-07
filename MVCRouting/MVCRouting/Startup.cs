using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCRouting.Startup))]
namespace MVCRouting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
