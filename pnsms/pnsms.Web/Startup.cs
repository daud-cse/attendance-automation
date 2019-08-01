using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pnsms.erp.Startup))]
namespace pnsms.erp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
