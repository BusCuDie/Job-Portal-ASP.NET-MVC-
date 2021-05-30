using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TUYENDUNGVIECLAM.Startup))]
namespace TUYENDUNGVIECLAM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
