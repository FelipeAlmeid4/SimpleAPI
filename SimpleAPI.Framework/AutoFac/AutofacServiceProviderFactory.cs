using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using SimpleAPI.Framework.Extensions;

namespace SimpleAPI.Framework.AutoFac
{
    public class AutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly Action<ContainerBuilder> configurationAction;

        public AutofacServiceProviderFactory(Action<ContainerBuilder> configurationAction = null)
        {
            this.configurationAction = configurationAction ?? (builder => { });
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>();
            builder.RegisterType<AutofacServiceScopeFactory>().As<IServiceScopeFactory>();
            builder.Register(services);
            configurationAction(builder);

            return builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            if (containerBuilder == null) throw new ArgumentNullException(nameof(containerBuilder));
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
