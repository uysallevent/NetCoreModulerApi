

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ModulerApi.Shared
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}