using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(deepp.landing.Startup))]
namespace deepp.landing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
