using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.Framework.Utils.Pluralizers;
using SimpleAPI.WebFramework.AppBuilder;
using SimpleAPI.WebFramework.Extensions;

namespace SimpleAPI.WebFramework.Mvc.Conventions
{
    public class ConventionPlugin : IAppPlugin
    {
        public void Configure(IAppBuilder builder)
        {
            builder.Services.AddScoped<IPluralizerService, PluralizerService>();
            builder.Services.AddSingleton<IActionModelConvention, ActionModelConvention>();

            builder.Services.Configure<MvcOptions, IActionModelConvention>((opts, convention) =>
            {
                opts.Conventions.Add(convention);
                opts.Conventions.Add(new ControllerModelConvention());
                opts.Conventions.Add(new ParameterModelConvention());
            });
        }
    }
}
