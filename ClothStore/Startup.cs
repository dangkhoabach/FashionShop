using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClothStore.Startup))]
namespace ClothStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
