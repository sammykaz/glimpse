using Microsoft.Owin;
using Owin;
using WebServices;

[assembly: OwinStartup(typeof(Startup))]

namespace WebServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}