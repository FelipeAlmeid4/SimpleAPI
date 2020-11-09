using Domain.Core.Repositories;
using Infrastructure.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.WebFramework.AppBuilder;

namespace Infrastructure.Core.Data
{
    public class NhibernatePlugin : IAppPlugin
    {
        public void Configure(IAppBuilder builder)
        {
            builder.Services.AddSingleton<ISessionFactoryBuilder, FluentSessionFactoryBuilder>();
            builder.Services.AddSingleton((c) => c.GetService<ISessionFactoryBuilder>().BuildSessionFactory());
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            builder.Services.AddScoped<ITransactionManager, TransactionManager>();
            builder.Services.AddScoped<IDataContext, DataContext>();
            builder.Services.AddScoped<ISessionProvider, SessionProvider>();
            builder.Services.Configure<DataOptions>(builder.Configuration.GetSection("DataOptions"));
        }
    }
}
