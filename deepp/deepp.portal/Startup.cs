using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(deepp.portal.Startup))]
namespace deepp.portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
