using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pnsms.landing.Startup))]
namespace pnsms.landing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
