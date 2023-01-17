using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using WPFLocalizeExtension.Providers;

namespace CommonLib23.Infrastructure.Localization
{
    public class CustomLocalizationProvider : ResxLocalizationProvider
    {
        private List<Assembly> assemblies;

        public CustomLocalizationProvider()
        {
            this.LoadAssemblies();
        }

        private void LoadAssemblies()
        {
            this.assemblies = LocalizationContext.Assemblies;
        }

        public override object GetLocalizedObject(string key, DependencyObject target, CultureInfo culture)
        {
            var splitted = key.Split(':');
            var formattedKey = splitted.Length == 1 ? splitted[0] : splitted[2];

            foreach (var assembly in this.assemblies)
            {
                var newKey = $"{assembly.GetName().Name}:Strings:{formattedKey}";
                var localizedObject = base.GetLocalizedObject(newKey, target, culture);
                
                if (localizedObject != null)
                    return localizedObject;
            }

            return $"Resource with key '{formattedKey}' not found";
        }
    }
}
