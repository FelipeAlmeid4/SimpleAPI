using System.Collections.Generic;
using System.Reflection;

namespace SimpleAPI.Framework.Helpers
{
    public static class AssemblyHelper
    {
        public static IList<Assembly> Load(string[] assemblies)
        {
            var _assemblies = new List<Assembly>();

            foreach (var item in assemblies)
            {
                if (Assembly.Load(item) is Assembly _assembly)
                {
                    _assemblies.Add(_assembly);
                }
            }

            return _assemblies;
        }
    }
}
