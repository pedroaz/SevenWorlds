using Microsoft.Owin.Cors;
using Owin;

namespace SevenWorlds.GameServer.Startups
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
