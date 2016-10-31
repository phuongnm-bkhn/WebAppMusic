using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppMusic.Startup))]
namespace WebAppMusic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
