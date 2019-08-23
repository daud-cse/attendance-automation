using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pnsms.landingTest.Startup))]
namespace pnsms.landingTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
