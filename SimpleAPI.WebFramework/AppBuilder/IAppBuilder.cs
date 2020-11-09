using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleAPI.WebFramework.AppBuilder
{
    public interface IAppBuilder
    {
        IConfiguration Configuration { get; }
        IServiceCollection Services { get; }
        AppModuleCollection Modules { get; }
        AppPluginCollection Plugins { get; }
        AppFilterActionCollection Filters { get; }
        void AddModule<TModule>() where TModule : IAppModule;
        void AddPlugin(IAppPlugin plugin);
        void AddFilter<TFilterAction>() where TFilterAction : IFilterMetadata;
    }
}
