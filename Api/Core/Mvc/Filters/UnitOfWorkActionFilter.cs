using Application.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleAPI.WebFramework.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Mvc.Filters
{
    public class UnitOfWorkActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public UnitOfWorkActionFilter(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
            {
                await next();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();
            var attr = methodInfo.GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().FirstOrDefault();

            if (attr != null && !attr.Enabled)
            {
                await next();
                return;
            }

            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                var result = await next();

                if (result.Exception == null || result.ExceptionHandled)
                {
                    await unitOfWork.CompleteAsync();
                }
            }
        }
    }
}
