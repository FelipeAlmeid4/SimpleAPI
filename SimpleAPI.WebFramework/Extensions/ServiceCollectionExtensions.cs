using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleAPI.WebFramework.AppBuilder;
using SimpleAPI.WebFramework.Mvc.DependencyWrapper;
using System;

namespace SimpleAPI.WebFramework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Configure<TOptions, TDependency>(this IServiceCollection serviceCollection, Action<TOptions, TDependency> configureOptions) where TOptions : class, new()
        {
            serviceCollection.AddSingleton<IConfigureOptions<TOptions>, ConfigureOptionsWithDependencyWrapper<TOptions, TDependency>>();
            serviceCollection.Configure<ConfigureOptionsWithDependency<TOptions, TDependency>>(options => { options.Action = configureOptions; });
            return serviceCollection;
        }

        public static void AddAppBuilder(this IServiceCollection services, IConfiguration configuration, Action<IAppBuilder> action)
        {
            var builder = new SimpleAPI.WebFramework.AppBuilder.AppBuilder(services, configuration);
            action.Invoke(builder);
            builder.RegisterServices();
            services.AddSingleton(typeof(IAppBuilder), builder);
        }
    }
}
