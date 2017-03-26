using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteSushi.Startup))]
namespace WebSiteSushi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
