using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SimpleAPI.Framework.Extensions;
using System.Linq;

namespace SimpleAPI.WebFramework.Mvc.Conventions
{
    public class ControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ApiExplorer.GroupName.IsNullOrEmpty())
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }

            if (controller.ApiExplorer.IsVisible == null)
            {
                controller.ApiExplorer.IsVisible = true;
            }

            controller.Selectors
                .Where(c => c.AttributeRouteModel == null && c.ActionConstraints.IsNullOrEmpty())
                .ToList()
                .ForEach(c => controller.Selectors.Remove(c));
        }
    }
}
