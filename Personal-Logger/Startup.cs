using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalLogger.Startup))]
namespace PersonalLogger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
