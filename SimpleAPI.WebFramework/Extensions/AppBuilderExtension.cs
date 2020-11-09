using SimpleAPI.WebFramework.AppBuilder;
using SimpleAPI.WebFramework.Mvc.Conventions;

namespace SimpleAPI.Framework.Extensions
{
    public static class AppBuilderExtension
    {
        public static void AddConvention(this IAppBuilder builder)
        {
            builder.AddPlugin(new ConventionPlugin());
        }
    }
}
