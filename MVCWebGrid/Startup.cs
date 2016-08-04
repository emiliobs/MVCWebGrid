using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWebGrid.Startup))]
namespace MVCWebGrid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
