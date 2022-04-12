using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StartupEd.UX.One;
using StartupEd.Lib.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StartupEd.Lib.Core;

namespace StartupEd.UX.Incubation
{
    public class Program : BaseProgram
    {
        public static async Task Main(string[] args)
        {

            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
          
            Services = builder.Services;
            Configuration = builder.Configuration;
            HostAddess = builder.HostEnvironment.BaseAddress;
            SLog.Write("ASP.NET Core Hosted at: " + HostAddess);
            SetServices();
            await builder.Build().RunAsync();
        }
    }
}


