using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleAPI.WebFramework.AppBuilder
{
    public class AppBuilder : IAppBuilder
    {
        public IConfiguration Configuration { get; }
        public IServiceCollection Services { get; }
        public AppModuleCollection Modules { get; }
        public AppPluginCollection Plugins { get; }
        public AppFilterActionCollection Filters { get; }

        public AppBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
            Modules = new AppModuleCollection();
            Plugins = new AppPluginCollection();
            Filters = new AppFilterActionCollection();
        }

        public void AddModule<TModule>()
            where TModule : IAppModule
        {
            Modules.Add(typeof(TModule));
        }

        public void AddPlugin(IAppPlugin plugin)
        {
            Plugins.Add(plugin);
        }

        public void AddFilter<TFilterAction>()
            where TFilterAction : IFilterMetadata
        {
            Filters.Add(typeof(TFilterAction));
        }

        public void RegisterServices()
        {
            RegisterPluginsServices();
            RegisterModulesServices();
            RegisterFiltersServices();
        }

        private void RegisterPluginsServices()
        {
            foreach (var plugin in Plugins)
            {
                plugin.Configure(this);
            }
        }

        private void RegisterFiltersServices()
        {
            foreach (var filter in Filters)
            {
                Services.AddScoped(filter);

                Services.Configure<MvcOptions>(opts =>
                {
                    opts.Filters.AddService(filter);
                });
            }
        }

        private void RegisterModulesServices()
        {
            foreach (var moduleType in Modules)
            {
                ((IAppModule)Activator.CreateInstance(moduleType))?.ConfigureServices(Services);
            }
        }
    }
}
