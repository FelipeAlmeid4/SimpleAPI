using Autofac;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.Framework.Common;
using System;

namespace SimpleAPI.Framework.AutoFac
{
    public class AutofacServiceProvider : Disposable, IServiceProvider, ISupportRequiredService
    {
        public ILifetimeScope LifetimeScope { get; }

        public AutofacServiceProvider(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
        }

        public object GetRequiredService(Type serviceType)
        {
            return LifetimeScope.Resolve(serviceType);
        }

        public object GetService(Type serviceType)
        {
            return LifetimeScope.ResolveOptional(serviceType);
        }

        protected override void OnDispose()
        {
            LifetimeScope.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
