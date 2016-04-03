using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KS.SportsPool.MVC.Startup))]
namespace KS.SportsPool.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
