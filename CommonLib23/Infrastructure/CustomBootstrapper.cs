using CommonLib23.Infrastructure.Localization;
using CommonLib23.Views.ShellWindow;
using CommonLib23.Views.Users;
using Stylet;
using StyletIoC;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using WPFLocalizeExtension.Engine;

namespace CommonLib23.Infrastructure
{
    public abstract class CustomBootstrapper : Bootstrapper<ShellViewModel>
    {
        private List<Assembly> customAssemblies = new List<Assembly>();
        private string culture;

        protected void AddAssemblyAsCustomModuleAssembly(Assembly assembly)
        {
            customAssemblies.Add(assembly);
        }

        protected void SetCulture(string culture)
        {
            this.culture = culture;
        }

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            // Add current assembly to IoC builder
            var thisAssembly = typeof(ShellViewModel).Assembly;
            builder.Assemblies.Add(thisAssembly);

            builder.Bind<IScreenViewModelFactory>().ToAbstractFactory();

            builder.Bind<IScreen>().To<UsersViewModel>().WithKey("users");
        }

        protected override void Configure()
        {
            base.Configure();

            ConfigureLocalizationProvider();
        }

        private void ConfigureLocalizationProvider()
        {
            // Resource Manager from current assembly
            LocalizationContext.AddAssembly(Assembly.GetExecutingAssembly());

            // Resource Manager from TP20.Client.Desktop.Core
            LocalizationContext.AddAssembly(typeof(ShellViewModel).Assembly);

            // Resource managers from custom assemblies
            foreach (var customAssembly in customAssemblies)
                LocalizationContext.AddAssembly(customAssembly);
        }

        protected override void Launch()
        {
            SetUserEnvironment();
            DisplayRootView(RootViewModel);
        }

        private void SetUserEnvironment()
        {
            // Set culture
            LocalizeDictionary.Instance.Culture
                    = Thread.CurrentThread.CurrentCulture
                    = Thread.CurrentThread.CurrentUICulture
                    = new CultureInfo(culture);
        }
    }
}
