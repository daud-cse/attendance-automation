using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sfa.website.Startup))]
namespace sfa.website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
