using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleAPI.Framework.Extensions;
using SimpleAPI.Framework.Helpers;
using System;
using System.Linq;

namespace SimpleAPI.WebFramework.Mvc.Conventions
{
    public class ParameterModelConvention : IParameterModelConvention
    {
        public void Apply(ParameterModel param)
        {
            if (param.BindingInfo != null || param.Name == "id")
            {
                return;
            }

            param.BindingInfo = BindingInfo.GetBindingInfo(new[] {
                        !TypeHelper.IsPrimitiveExtendedIncludingNullable(param.ParameterInfo.ParameterType) && CanUseFormBodyBinding(param) ?
                        new FromBodyAttribute() :
                        (Attribute)new FromQueryAttribute() });
        }

        private bool CanUseFormBodyBinding(ParameterModel parameter)
        {
            foreach (var selector in parameter.Action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    if (!(actionConstraint is HttpMethodActionConstraint httpMethodActionConstraint))
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.In("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
