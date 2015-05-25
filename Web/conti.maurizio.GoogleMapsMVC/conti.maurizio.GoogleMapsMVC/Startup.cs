using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(conti.maurizio.GoogleMapsMVC.Startup))]
namespace conti.maurizio.GoogleMapsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
