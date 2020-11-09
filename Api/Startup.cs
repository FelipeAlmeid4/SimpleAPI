using Api.Core;
using Api.Core.Mvc.Filters;
using Application.Core.Configurations;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleAPI.Framework.Extensions;
using SimpleAPI.WebFramework.Extensions;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAppBuilder(Configuration, (builder) =>
            {
                builder.AddNhibernate();
                builder.AddUnitOfWork();
                builder.AddConvention();
                builder.AddAutoMapper("Api");
                builder.AddModule<ServicesModule>();
                builder.AddFilter<UnitOfWorkActionFilter>();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterModule(new MyApplicationModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
