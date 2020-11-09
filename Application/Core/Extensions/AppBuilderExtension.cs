using Application.Core.UnitOfWork;
using AutoMapper;
using Infrastructure.Core.Data;
using SimpleAPI.WebFramework.AppBuilder;
using SimpleAPI.Framework.Helpers;

namespace Application.Core.Configurations
{
    public static class AppBuilderExtension
    {
        public static void AddNhibernate(this IAppBuilder builder)
        {
            builder.AddPlugin(new NhibernatePlugin());
        }

        public static void AddAutoMapper(this IAppBuilder builder, params string[] assemblies)
        {
            builder.Services.AddAutoMapper(AssemblyHelper.Load(assemblies));
        }

        public static void AddUnitOfWork(this IAppBuilder builder)
        {
            builder.AddPlugin(new UnitOfWorkPlugin());
        }
    }
}
