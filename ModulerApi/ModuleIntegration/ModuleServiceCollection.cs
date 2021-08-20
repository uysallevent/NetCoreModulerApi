using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using ModulerApi.Module.Shared;

namespace ModulerApi.ModuleIntegration
{
    public static class ModuleServiceCollection
    {
        public static IServiceCollection AddModule<TStartup>(this IServiceCollection services, string routePrefix)
            where TStartup : IStartup, new()
        {
            // Register assembly in MVC so it can find controllers of the module
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(TStartup).Assembly)));

            var startup = new TStartup();
            startup.ConfigureServices(services);

            services.AddSingleton(new Module(routePrefix, startup));

            return services;
        }
    }
}