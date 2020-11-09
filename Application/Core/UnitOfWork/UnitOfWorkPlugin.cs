using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.WebFramework.AppBuilder;

namespace Application.Core.UnitOfWork
{
    public class UnitOfWorkPlugin : IAppPlugin
    {
        public UnitOfWorkPlugin()
        { }

        public void Configure(IAppBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
