using Application.Application;
using Application.Contracts;
using Domain.Users;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.WebFramework.AppBuilder;

namespace Api.Core
{
    public class ServicesModule : IAppModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region ApplicationServices

            services.AddScoped<IUserAppService, UserAppService>();

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region Validators

            #endregion
        }
    }
}
