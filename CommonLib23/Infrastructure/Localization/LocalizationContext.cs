using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLib23.Infrastructure.Localization
{
    public static class LocalizationContext
    {
        private static Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

        public static void AddAssembly(Assembly assembly)
        {
            if (!assemblies.ContainsKey(assembly.FullName))
                assemblies.Add(assembly.FullName, assembly);
        }

        public static List<Assembly> Assemblies => assemblies.Values.ToList();
    }
}
