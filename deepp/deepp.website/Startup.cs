using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(deepp.website.Startup))]
namespace deepp.website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
