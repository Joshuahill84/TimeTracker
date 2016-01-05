using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeTracker.App.Startup))]
namespace TimeTracker.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
