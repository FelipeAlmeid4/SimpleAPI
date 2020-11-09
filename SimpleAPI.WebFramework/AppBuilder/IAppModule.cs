using Microsoft.Extensions.DependencyInjection;

namespace SimpleAPI.WebFramework.AppBuilder
{
    public interface IAppModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}
