using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(TudsWeb.Startup))]
namespace TudsWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
