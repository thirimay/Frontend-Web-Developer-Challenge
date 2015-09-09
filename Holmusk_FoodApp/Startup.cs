using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Holmusk_FoodApp.Startup))]
namespace Holmusk_FoodApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
