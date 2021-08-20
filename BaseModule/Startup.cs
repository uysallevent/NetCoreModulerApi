using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ModulerApi.BaseModule
{
    public class Startup : Module.Shared.IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/BaseModule",
                    async context =>
                    {
                        await context.Response.WriteAsync("Hello World from TestEndpoint in Module 2");
                    })
            );
        }
    }
}