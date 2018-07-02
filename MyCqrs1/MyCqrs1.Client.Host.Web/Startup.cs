using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCqrs1.Client.Host.Web.Startup))]
namespace MyCqrs1.Client.Host.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
