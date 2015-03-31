using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
