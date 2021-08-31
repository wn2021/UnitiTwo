using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnitiTwo.Startup))]
namespace UnitiTwo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
