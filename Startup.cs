using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lindaniDS.Startup))]
namespace lindaniDS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
