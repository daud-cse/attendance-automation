using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pnsms.portal.Startup))]
namespace pnsms.portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
