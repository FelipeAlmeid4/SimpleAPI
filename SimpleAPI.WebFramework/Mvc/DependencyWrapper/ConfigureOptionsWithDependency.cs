using System;

namespace SimpleAPI.WebFramework.Mvc.DependencyWrapper
{
    public class ConfigureOptionsWithDependency<TOptions, TDependency>
    {
        public Action<TOptions, TDependency> Action { get; set; }
    }
}
