using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SimpleAPI.Framework.Extensions;
using SimpleAPI.Framework.Utils.Pluralizers;
using System.Linq;

namespace SimpleAPI.WebFramework.Mvc.Conventions
{
    public class ActionModelConvention : IActionModelConvention
    {
        protected readonly string[] actionPrefixes = new string[] { "GetAll", "Get", "Put", "Update", "Patch", "Delete", "Remove", "Post", "Create", "Insert" };
        protected readonly string[] unpluralizables = new string[] { "auth" };
        protected readonly IPluralizerService pluralizationService;

        public ActionModelConvention(IPluralizerService pluralizationService)
        {
            this.pluralizationService = pluralizationService;
            this.pluralizationService.ConfigureUnpluralizables(unpluralizables);
        }

        public void Apply(ActionModel action)
        {
            var controllerName = GetControllerName(action.Controller);

            action.Selectors
                .Where(c => c.AttributeRouteModel == null && c.ActionConstraints.IsNullOrEmpty())
                .ToList()
                .ForEach(c => action.Selectors.Remove(c));

            if (action.Selectors.Any())
            {
                NormalizeSelectorRoutes(controllerName, action);
                return;
            }

            var verb = GetConventionalVerbForMethodName(action.ActionName);
            var selectorModel = new SelectorModel { AttributeRouteModel = CreateAttributeRoute(controllerName, verb, action) };
            selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb }));
            action.Selectors.Add(selectorModel);
        }

        protected virtual string GetControllerName(ControllerModel controller)
        {
            if (controller.RouteValues.ContainsKey("controller"))
            {
                return controller.RouteValues["controller"];
            }

            var controllerName = pluralizationService.Pluralize(controller.ControllerName);

            return controllerName.ToKebabCase();
        }

        protected virtual string GetConventionalVerbForMethodName(string actionName)
        {
            if (actionName.StartsWithIgnoreCase("Get"))
            {
                return "GET";
            }

            if (actionName.StartsWithIgnoreCase("Put") ||
                actionName.StartsWithIgnoreCase("Update"))
            {
                return "PUT";
            }

            if (actionName.StartsWithIgnoreCase("Patch"))
            {
                return "PATCH";
            }

            if (actionName.StartsWithIgnoreCase("Delete") ||
                actionName.StartsWithIgnoreCase("Remove"))
            {
                return "DELETE";
            }

            if (actionName.StartsWithIgnoreCase("Post") ||
                actionName.StartsWithIgnoreCase("Create") ||
                actionName.StartsWithIgnoreCase("Insert"))
            {
                return "POST";
            }

            return "POST";
        }

        protected virtual AttributeRouteModel CreateAttributeRoute(string controllerName, string verb, ActionModel action)
        {
            var conventionActionName = GetConventionActionName(action, verb);
            var conventionRouteTemplate = GetConventionRouteTemplate(controllerName, action, conventionActionName);
            return new AttributeRouteModel(new RouteAttribute(conventionRouteTemplate));
        }
        
        protected virtual string GetConventionActionName(ActionModel action, string verb)
        {
            var actionName = action.ActionName;
            var prefixFounded = false;

            if (!action.Attributes.Any(c => c is ActionNameAttribute actionNameAttribute && !actionNameAttribute.Name.IsNullOrEmpty()))
            {
                foreach (var prefix in actionPrefixes)
                {
                    if (action.ActionName.StartsWithIgnoreCase(prefix))
                    {
                        actionName = action.ActionName.RemoveBeginning(prefix);
                        prefixFounded = true;
                        break;
                    }
                }

                if (!prefixFounded)
                {
                    actionName = action.ActionName;
                }

                if (!actionName.IsNullOrEmpty())
                {
                    actionName = actionName.RemoveFromEndIgnoreCase("Async");
                }

                if (!actionName.IsNullOrEmpty())
                {
                    actionName = actionName.ToKebabCase();
                };
            }

            return actionName;
        }

        protected virtual string GetConventionRouteTemplate(string controllerName, ActionModel action, string routeActionName)
        {
            return string.Empty
                .ConcatStringIf(true, "api")
                .ConcatString($"/{controllerName.ToLower()}")
                .ConcatStringIf(action.Parameters.Any(c => c.Name == "id"), "/{id}")
                .ConcatStringIf(!routeActionName.IsNullOrEmpty(), $"/{routeActionName}");
        }

        private void NormalizeSelectorRoutes(string controllerName, ActionModel action)
        {
            foreach (var selector in action.Selectors.Where(c => c.AttributeRouteModel == null))
            {
                var verb = GetConventionalVerbForMethodName(action.ActionName);
                selector.AttributeRouteModel = CreateAttributeRoute(controllerName, verb, action);
            }
        }
    }
}
