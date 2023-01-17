using CommonLib23.Infrastructure;
using Stylet;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using WPFLocalizeExtension.Engine;

namespace CommonLib23.Views.ShellWindow
{
    public class ShellViewModel : Conductor<IScreen>
    {
        private readonly IScreenViewModelFactory screenViewModelFactory;

        public IScreen ContentViewModel { get; set; }
        public ICommand CreateViewCommand { get; set; }
        public string Culture { get; set; }

        public ShellViewModel(IScreenViewModelFactory screenViewModelFactory)
        {
            this.screenViewModelFactory = screenViewModelFactory;
            Culture = CultureInfo.CurrentUICulture.Name;
        }

        protected override void OnInitialActivate()
        {
            ActivateItem(ContentViewModel);
        }

        public void CreateUsersView()
        {
            var usersViewModel = screenViewModelFactory.CreateScreenViewModel("users");
            if (usersViewModel != null)
            {
                ContentViewModel = usersViewModel;
                ActiveItem = ContentViewModel;
                ActivateItem(ContentViewModel);
                ActivateItem(ActiveItem);
            }
        }

        public void CreateCustomersView()
        {
            var customersViewModel = screenViewModelFactory.CreateScreenViewModel("customers");
            if (customersViewModel != null)
            {
                ContentViewModel = customersViewModel;
                ActiveItem = ContentViewModel;
                ActivateItem(ContentViewModel);
                ActivateItem(ActiveItem);
            }
        }

        public void SwitchLanguage()
        {
            string newCulture = Culture == "ca-ES" ? "es-ES" : "ca-ES";
            Culture = newCulture;
            NotifyOfPropertyChange(nameof(Culture));

            // Set culture
            LocalizeDictionary.Instance.Culture
                    = Thread.CurrentThread.CurrentCulture
                    = Thread.CurrentThread.CurrentUICulture
                    = new CultureInfo(newCulture);
        }
    }
}
