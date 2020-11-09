using Autofac;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.Framework.Common;
using System;

namespace SimpleAPI.Framework.AutoFac
{
    public class AutofacServiceScope : Disposable, IServiceScope
    {
        public IServiceProvider ServiceProvider { get; }

        private readonly ILifetimeScope lifetimeScope;

        public AutofacServiceScope(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
            ServiceProvider = this.lifetimeScope.Resolve<IServiceProvider>();
        }

        protected override void OnDispose()
        {
            lifetimeScope.Dispose();
        }
    }
}
