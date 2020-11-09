using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleAPI.Framework.AutoFac
{
    public class AutofacServiceScopeFactory : IServiceScopeFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public AutofacServiceScopeFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IServiceScope CreateScope()
        {
            return new AutofacServiceScope(this.lifetimeScope.BeginLifetimeScope());
        }
    }
}
