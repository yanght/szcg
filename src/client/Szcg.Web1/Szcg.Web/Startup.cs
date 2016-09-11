using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Szcg.Web.Startup))]
namespace Szcg.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
