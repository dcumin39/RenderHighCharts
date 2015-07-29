using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RenderHighCharts.Startup))]
namespace RenderHighCharts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
        }
    }
}
