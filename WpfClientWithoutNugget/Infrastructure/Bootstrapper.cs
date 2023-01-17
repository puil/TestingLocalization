using CommonLib23.Infrastructure;
using Stylet;
using StyletIoC;
using System.Reflection;
using WpfClientWithoutNugget.Views.Customers;

namespace WpfClientWithoutNugget.Infrastructure
{
    public class Bootstrapper : CustomBootstrapper
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            AddAssemblyAsCustomModuleAssembly(Assembly.GetExecutingAssembly());
            SetCulture("ca-ES");

            builder.Bind<IScreen>().To<CustomersViewModel>().WithKey("customers");
        }
    }
}
