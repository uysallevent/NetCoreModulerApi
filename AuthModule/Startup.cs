using AuthModule.Security.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ModulerApi.AuthModule
{
    public class Startup : Module.Shared.IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenHelper, JwtHelper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/AuthModule",
                    async context =>
                    {
                        await context.Response.WriteAsync("Hello World from TestEndpoint in Module 2");
                    })
            );
        }
    }
}