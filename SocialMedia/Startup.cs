using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialMedia.Startup))]
namespace SocialMedia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
