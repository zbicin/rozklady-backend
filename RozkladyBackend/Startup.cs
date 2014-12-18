using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RozkladyBackend.Startup))]
namespace RozkladyBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
