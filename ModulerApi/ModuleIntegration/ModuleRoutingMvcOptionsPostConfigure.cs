using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ModulerApi.ModuleIntegration
{
    public class ModuleRoutingMvcOptionsPostConfigure : IPostConfigureOptions<MvcOptions>
    {
        private readonly IEnumerable<Module> _modules;

        public ModuleRoutingMvcOptionsPostConfigure(IEnumerable<Module> modules)
        {
            _modules = modules;
        }

        public void PostConfigure(string name, MvcOptions options)
        {
            options.Conventions.Add(new ModuleRoutingConvention(_modules));
        }
    }
}