using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chatter3.Startup))]
namespace Chatter3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
